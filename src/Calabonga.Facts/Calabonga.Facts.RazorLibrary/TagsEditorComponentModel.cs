using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calabonga.Facts.RazorLibrary
{
    public class TagsEditorComponentModel : ComponentBase
    {
        [Parameter]
        public List<string> Tags { get; set; }

        [Inject] 
        public IJSRuntime JsRuntime { get; set; }

        protected async Task DeleteTag(string tag)
        {
            if (string.IsNullOrEmpty(tag))
            {
                return;
            }

            var tagToDelete = Tags.SingleOrDefault(x => x == tag);
            if (tagToDelete is null)
            {
                return;
            }

            Tags.Remove(tag);

            // Calabonga: UPdate TAGCOUNT (2021-05-17 03:57 TagsEditorComponentModel)
            await new RazorInterop(JsRuntime).SetTagTotal(Tags.Count);
        }
    }
}
