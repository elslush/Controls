using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Controls.Scrolling
{
    public class ScrollHandler
    {
        private readonly IJSObjectReference module;

        public static async Task<ScrollHandler> New(IJSRuntime js)
        {
            var module = await js.InvokeAsync<IJSObjectReference>("import", "./_content/Controls/scroll.js");
            return new(module);
        }

        private ScrollHandler(IJSObjectReference module) => this.module = module;

        public Task ScrollTo(ElementReference element) => module.InvokeVoidAsync("scrollTo", element).AsTask();
    }
}
