using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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

            Founded = SearchService.SearchContent(args.Value.ToString());

            if (Founded.Any())
            {
                Parallel.ForEach(Founded, x => ReplaceTerm(x, args.Value!.ToString()));
            }
        }

        private void ReplaceTerm(FactViewModel fact, string term)
        {
            var regex = new Regex(term, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            var value = fact.Content;
            if (regex.IsMatch(value))
            {
                fact.Content = regex.Replace(value, "<strong><mark>" + term + "</mark></strong>");
            }
        }
    }
}
