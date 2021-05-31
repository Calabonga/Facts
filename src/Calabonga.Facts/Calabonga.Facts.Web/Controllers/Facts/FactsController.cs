using Calabonga.Facts.Web.Controllers.Facts.Command;
using System.Threading.Tasks;
using Calabonga.Facts.Web.Controllers.Facts.Queries;
using Calabonga.Facts.Web.Infrastructure;
using Calabonga.Facts.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Calabonga.Facts.Web.Controllers.Facts
{
    public class FactsController : Controller
    {
        private readonly IMediator _mediator;

        public FactsController(IMediator mediator) => _mediator = mediator;

        public async Task<IActionResult> Index(int? pageIndex,
            string tag,
            string search)
        {
            ViewData["search"] = search;
            ViewData["tag"] = tag;
            var index = pageIndex ?? 1;
            var operation = await _mediator.Send(new FactGetPagedRequest(index, tag, search), HttpContext.RequestAborted);
            if (operation.Ok && operation.Result.TotalPages < index)
            {
                return RedirectToAction(nameof(Index), new
                {
                    tag,
                    search,
                    pageIndex = 1
                });
            }

            return View(operation);
        }

        [Authorize(Roles = AppData.AdministratorRoleName)]
        public async Task<IActionResult> Edit(Guid id, string returnUrl)
        {
            var operationResult = await _mediator.Send(new FactGetByIdForEditRequest(id, returnUrl));
            if (operationResult.Ok)
            {
                return View(operationResult.Result);
            }

            return RedirectToAction("Error", "Site", new
            {
                code = 404
            });
        }

        [HttpPost]
        [Authorize(Roles = AppData.AdministratorRoleName)]
        public async Task<IActionResult> Edit(FactEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var operationResult = await _mediator.Send(new FactUpdateRequest(model));
                if (operationResult.Ok)
                {
                    return string.IsNullOrEmpty(model.ReturnUrl)
                        ? RedirectToAction("Index", "Facts")
                        : Redirect(model.ReturnUrl);
                }
            }

            return View(model);
        }

        public IActionResult Cloud() => View();

        public async Task<IActionResult> Rss() =>
            Content(await _mediator.Send(new FactGetRssRequest(), HttpContext.RequestAborted));

        public async Task<IActionResult> Random() =>
            View(await _mediator.Send(new FactGetRandomRequest(), HttpContext.RequestAborted));

        public async Task<IActionResult> Show(Guid id,
            string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(await _mediator.Send(new FactGetByIdRequest(id), HttpContext.RequestAborted));
        }
    }
}