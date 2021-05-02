using System.Threading.Tasks;
using Calabonga.Facts.Web.Controllers.Facts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Calabonga.Facts.Web.Controllers.Facts
{
    public class FactsController : Controller
    {
        private readonly IMediator _mediator;

        public FactsController(IMediator mediator)
            => _mediator = mediator;

        public async Task<IActionResult> Index(int? pageIndex, string tag, string search)
        {
            ViewData["search"] = search;
            ViewData["tag"] = tag;
            var index = pageIndex ?? 1;
            var operation = await _mediator.Send(new FactGetPagedRequest(index, tag, search), HttpContext.RequestAborted);
            if (operation.Ok && operation.Result.TotalPages < index)
            {
                return RedirectToAction(nameof(Index), new {tag, search, pageIndex = 1});
            }

            return View(operation);
        }

        public IActionResult Random() => View();

        public async Task<IActionResult> Show(Guid id, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(await _mediator.Send(new FactGetByIdRequest(id), HttpContext.RequestAborted));
        }
    }
}