using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Calabonga.Facts.Web.ViewModels;

namespace Calabonga.Facts.Web.Controllers
{
    public class SiteController : Controller
    {
        public IActionResult About() => View();
        
        // Calabonga: WHAT I MADE 7
        public IActionResult Random() => View();
        
        // Calabonga: WHAT I MADE 8
        public IActionResult Cloud() => View();
        
        // Calabonga: WHAT I MADE 9
        public IActionResult Feedback() => View();
        
        // Calabonga: WHAT I MADE 10
        public IActionResult Rss() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}