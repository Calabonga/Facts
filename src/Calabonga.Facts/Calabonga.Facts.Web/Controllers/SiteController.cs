using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Calabonga.Facts.Web.ViewModels;

namespace Calabonga.Facts.Web.Controllers
{
    public class SiteController : Controller
    {
        public IActionResult About() => View();
        
        // Calabonga: WHAT I DID
        public IActionResult Random() => View();
        
        public IActionResult Cloud() => View();
        
        public IActionResult Feedback() => View();

        public IActionResult Rss() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}