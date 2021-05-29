using Calabonga.Facts.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
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

        [Inject] private ITagSearchService TagSearchService { get; set; }

        /// <summary>
        /// Represents items that will be found by search
        /// </summary>
        protected List<string> Founded { get; set; }

        /// <summary>
        /// // Calabonga: update summary (2021-05-29 03:08 TagsEditorComponentModel)
        /// </summary>
        protected string TagName { get; set; }

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

        protected void SearchTags(ChangeEventArgs args)
        {
            if (args?.Value is null)
            {
                Founded = null;
                return;
            }

            if (string.IsNullOrEmpty(args.Value.ToString()))
            {
                Founded = null;
                return;
            }

            Founded = TagSearchService.SearchTags(args.Value.ToString());
        }

        protected async Task AddTag(string value)
        {
            var tag = value?.ToLower().Trim();
            if (string.IsNullOrEmpty(tag))
            {
                return;
            }

            Tags ??= new List<string>();

            if (!Tags.Exists(x=>x.Equals(tag, StringComparison.InvariantCulture)))
            {
                Tags.Add(tag);
                // notify TagsTotal changed
                await new RazorInterop(JsRuntime).SetTagTotal(Tags.Count);
            }

            TagName = string.Empty;
            Founded = null;
        }
    }
}
