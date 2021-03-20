using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Calabonga.Facts.Web.ViewModels;
using MediatR;

namespace Calabonga.Facts.Web.Controllers
{
    public class SiteController : Controller
    {


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
