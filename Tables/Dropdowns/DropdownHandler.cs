using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Dropdowns
{
    internal class DropdownHandler : IAsyncDisposable
    {
        private readonly IJSObjectReference module;
        private IJSObjectReference? eventListenerFunction;
        private readonly List<IDisposable> eventReferences = new();
        private readonly List<IJSObjectReference> eventAsyncReferences = new();

        public static async Task<DropdownHandler> New(IJSRuntime js)
        {
            var module = await js.InvokeAsync<IJSObjectReference>("import", "./_content/Controls/floating-ui.js");
            return new(module);
        }

        private DropdownHandler(IJSObjectReference module) => this.module = module;

        public async Task AddCloseEventListeners(ElementReference dropdownReference)
        {
            eventListenerFunction = await module.InvokeAsync<IJSObjectReference>("addCloseEventListeners", dropdownReference);
        }

        public async ValueTask DisposeAsync()
        {
            if (module is not null)
                await module.DisposeAsync();
            eventReferences.ForEach(disposable => disposable.Dispose());
            foreach (var disposable in eventAsyncReferences)
            {
                await disposable.InvokeVoidAsync("stop");
                await disposable.DisposeAsync();
            }
            GC.SuppressFinalize(this);
        }
    }
}
