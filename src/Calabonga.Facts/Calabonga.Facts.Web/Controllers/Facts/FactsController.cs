using System.Threading.Tasks;

using Calabonga.Facts.Web.Controllers.Facts.Queries;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Calabonga.Facts.Web.Controllers.Facts
{
    public class FactsController : Controller
    {
        private readonly IMediator _mediator;

        public FactsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index(int? pageIndex, string tag, string search)
        {
            var operationResult = await _mediator.Send(new FactGetPagedRequest(pageIndex ?? 1, tag, search), HttpContext.RequestAborted);
            return View(operationResult);
        }
    }
}
