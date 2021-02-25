using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Calabonga.Facts.Web.Data;
using Calabonga.Facts.Web.ViewModels;

namespace Calabonga.Facts.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index([FromServices] ApplicationDbContext context)
        {
            using var transaction = context.Database.BeginTransaction();

            var fact1 = new Fact
            {
                Tags = new List<Tag> { new() { Name = "Tag1" }, new() { Name = "Tag2" } }
            };

            var fact2 = new Fact
            {
                Tags = new List<Tag> { new() { Name = "Tag3" }, new() { Name = "Tag4" } }
            };


            context.AddRange(fact1, fact2);

            context.SaveChanges();

            transaction.Rollback();
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
