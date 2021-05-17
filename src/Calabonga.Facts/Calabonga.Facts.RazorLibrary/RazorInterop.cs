using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Calabonga.Facts.RazorLibrary
{
    // This class provides an example of how JavaScript functionality can be wrapped
    // in a .NET class for easy consumption. The associated JavaScript module is
    // loaded on demand when first needed.
    //
    // This class can be registered as scoped DI service and then injected into Blazor
    // components for use.

    public class RazorInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

        public RazorInterop(IJSRuntime jsRuntime) => 
            _moduleTask = new Lazy<Task<IJSObjectReference>>(() =>
                jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Calabonga.Facts.RazorLibrary/razorLibrary.js")
                    .AsTask());

        public async ValueTask SetTagTotal(int value)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("setTagsTotal", "TotalTags", value);
        }   
        
        public async ValueTask<string> ShowToast(string message, string title, string type = "info")
        {
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<string>("showToast", message, title, type);
        }    
        
        public async ValueTask<string> CopyToClipboard(string value)
        {
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<string>("copyToClipboard", value);
        }

        public async ValueTask DisposeAsync()
        {
            if (_moduleTask.IsValueCreated)
            {
                var module = await _moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}