using System.Collections.Generic;
using Calabonga.Facts.Contracts;
using Microsoft.AspNetCore.Components;

namespace Calabonga.Facts.RazorLibrary
{
    public class SearchContentComponentModel : ComponentBase
    {
        protected List<FactViewModel> Founded { get; set; }

        [Inject] private ISearchService SearchService { get; set; }

        protected void SearchContent(ChangeEventArgs args)
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

            if (args.Value!.ToString()!.Length > 3)
            {
                Founded = SearchService.SearchContent(args.Value.ToString());
            }
        }
    }
}
