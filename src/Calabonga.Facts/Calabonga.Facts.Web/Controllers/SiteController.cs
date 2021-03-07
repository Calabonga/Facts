using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using Calabonga.Facts.Web.ViewModels;
using MediatR;

namespace Calabonga.Facts.Web.Controllers
{
    public class SiteController : Controller
    {
        private readonly IMediator _mediator;

        public SiteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index(int? pageIndex, string tag, string search)
        {
            ViewData["Index"] = pageIndex;
            ViewData["Tag"] = tag;
            ViewData["Search"] = search;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
