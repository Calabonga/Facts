using Calabonga.Facts.Web.Infrastructure.TagHelpers.PagedListTagHelper.Base;

namespace Calabonga.Facts.Web.Infrastructure.TagHelpers.PagedListTagHelper
{
    public class PagerPageActive : PagerPageBase
    {
        public PagerPageActive(string title, int value) : base(title, value, true)
        {
        }
    }
}