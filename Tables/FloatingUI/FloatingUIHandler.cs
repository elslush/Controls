using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Controls.FloatingUI
{
    public class FloatingUIHandler : IAsyncDisposable
    {
        private readonly IJSObjectReference module, computePositionFunction;
        private IJSObjectReference? eventListenerFunction;
        private readonly List<IDisposable> eventReferences = new();

        public static async Task<FloatingUIHandler> New(IJSRuntime js, FloatingUIOptions floatingUIOptions)
        {
            var module = await js.InvokeAsync<IJSObjectReference>("import", "./_content/Controls/floating-ui.js");
            var computePositionFunction = await module.InvokeAsync<IJSObjectReference>("buildComputePosition", floatingUIOptions);
            return new(module, computePositionFunction);
        }

        private FloatingUIHandler(IJSObjectReference module, IJSObjectReference computePositionFunction)
        {
            this.module = module;
            this.computePositionFunction = computePositionFunction;
        }

        public ValueTask<Result> GetPosition(ElementReference buttonReference, ElementReference tooltipReference) =>
            computePositionFunction.InvokeAsync<Result>("computePosition", buttonReference, tooltipReference);

        public async Task AddEventListeners(Action function, ElementReference buttonReference, ElementReference tooltipReference)
        {
            var eventReference = DotNetObjectReference.Create(new ActionReference(function));
            eventReferences.Add(eventReference);
            eventListenerFunction = await module.InvokeAsync<IJSObjectReference>("addEventListeners", eventReference, buttonReference, tooltipReference);
        }

        public async Task AddEventListeners(Func<Task> function, ElementReference buttonReference, ElementReference tooltipReference)
        {
            var eventReference = DotNetObjectReference.Create(new TaskReference(function));
            eventReferences.Add(eventReference);
            eventListenerFunction = await module.InvokeAsync<IJSObjectReference>("addEventListeners", eventReference, buttonReference, tooltipReference);
        }

        private class ActionReference
        {
            private readonly Action function;

            public ActionReference(Action function) => this.function = function;

            [JSInvokable]
            public void Update() => function.Invoke();
        }

        private class TaskReference
        {
            private readonly Func<Task> function;

            public TaskReference(Func<Task> function) => this.function = function;

            [JSInvokable]
            public Task Update() => function.Invoke();
        }

        public class Result
        {
            [JsonIgnore]
            public string LeftStyle => $"{X}px";

            [JsonIgnore]
            public string TopStyle => $"{Y}px";

            [JsonPropertyName("x")]
            public float X { get; set; }

            [JsonPropertyName("y")]
            public float Y { get; set; }

            [JsonPropertyName("middlewareData")]
            public MiddlewareData? MiddlewareData { get; set; }
        }

        public class MiddlewareData
        {
            [JsonPropertyName("arrow")]
            public Result? Arrow { get; set; }

            [JsonPropertyName("hide")]
            public HideData? Hide { get; set; }

            public class HideData
            {
                [JsonPropertyName("referenceHidden")]
                public bool ReferenceHidden { get; set; }
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (module is not null)
                await module.DisposeAsync();
            if (computePositionFunction is not null)
                await computePositionFunction.DisposeAsync();
            if (eventListenerFunction is not null)
            {
                await eventListenerFunction.InvokeVoidAsync("stop");
                await eventListenerFunction.DisposeAsync();
            }
            eventReferences.ForEach(disposable => disposable.Dispose());
            GC.SuppressFinalize(this);
        }
    }
}
