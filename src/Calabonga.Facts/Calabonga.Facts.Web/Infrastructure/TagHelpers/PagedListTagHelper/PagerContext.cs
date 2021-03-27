namespace Calabonga.Facts.Web.Infrastructure.TagHelpers.PagedListTagHelper
{
    public class PagerContext
    {
        public int GroupIndex { get; set; }

        public int MinPage { get; set; }

        public int MaxPage { get; set; }

        public int NextPage { get; set; }

        public int PreviousPage { get; set; }

        public int TotalPages { get; set; }

        public int PageIndex { get; set; }
    }
}
