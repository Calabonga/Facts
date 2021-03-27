using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Calabonga.Facts.Web.Infrastructure.TagHelpers.PagedListTagHelper
{
    [HtmlTargetElement("pager", Attributes = TextAttributeName)]
    public class PagedListTagHelper : TagHelper
    {
        private const string TextAttributeName = "asp-text";

        [HtmlAttributeName(TextAttributeName)]
        public string Text { get; set; }

        /// <summary>
        /// Synchronously executes the <see cref="T:Microsoft.AspNetCore.Razor.TagHelpers.TagHelper" /> with the given <paramref name="context" /> and
        /// <paramref name="output" />.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.Append(Text);
            base.Process(context, output);
        }
    }
}
