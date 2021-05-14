using Calabonga.Facts.Web.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Calabonga.Facts.Web.ViewComponents
{
    public class CloudViewComponent: ViewComponent
    {
        private readonly ITagService _tagService;

        public CloudViewComponent(ITagService tagService) => _tagService = tagService;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var tags = await _tagService.GetCloudAsync();
            return View(tags);
        }
    }
}
