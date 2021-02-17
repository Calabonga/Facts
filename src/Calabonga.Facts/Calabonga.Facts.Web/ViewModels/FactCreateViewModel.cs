using System.Collections.Generic;

namespace Calabonga.Facts.Web.ViewModels
{
    public class FactCreateViewModel
    {
        public string Content { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}