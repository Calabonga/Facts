using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Calabonga.Facts.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calabonga.Facts.Web.Controllers
{
    public class SiteController : Controller
    {
        private readonly IMediator _mediator;
        private readonly List<SelectListItem> _subjects;

        // Calabonga: WHAT I DID
        public SiteController(IMediator mediator)
        {
            _mediator = mediator;
            _subjects = new List<string> {
                    "Связь с разработчиком",
                    "Жалоба",
                    "Предложение",
                    "Другое"
                }.Select(x => new SelectListItem { Value = x, Text = x })
                .ToList();
        }
        public IActionResult About() => View();
        
        public IActionResult Random() => View();
        
        public IActionResult Cloud() => View();
        
        // Calabonga: WHAT I DID
        public IActionResult Feedback()
        {
            ViewData["Subjects"] = _subjects;
            return View();
        }

        [HttpPost]
        public IActionResult Feedback(FeedbackViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Calabonga: not implemented (2021-04-17 09:01 SiteController)
                    //await _mediator.Publish(new FeedbackMessageNotification(model));
                    return RedirectToAction("FeedbackSent", "Site");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("_FORM", "Извините, не могу отправить сообщение:\n" + ex.Message);
                }
            }
            ViewData["subjects"] = _subjects;
            return View(model);
        }

        // Calabonga: WHAT I DID
        public IActionResult FeedbackSent() => View();

        public IActionResult Rss() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}