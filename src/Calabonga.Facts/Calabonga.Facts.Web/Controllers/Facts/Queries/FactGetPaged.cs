using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using Calabonga.AspNetCore.Controllers.Base;
using Calabonga.Facts.Web.Data;
using Calabonga.Facts.Web.ViewModels;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;

using Microsoft.EntityFrameworkCore;

namespace Calabonga.Facts.Web.Controllers.Facts.Queries
{
    public class FactGetPagedRequest : OperationResultRequestBase<IPagedList<FactViewModel>>
    {
        public FactGetPagedRequest(int pageIndex, string? tag, string? search)
        {
            PageIndex = pageIndex - 1 < 0 ? 0 : pageIndex - 1;
            Search = search;
            Tag = tag;
        }

        public int PageIndex { get; }

        public int PageSize => 20;

        public string? Tag { get; }

        public string? Search { get; }
    }

    public class FactGetPagedRequestHandler : OperationResultRequestHandlerBase<FactGetPagedRequest, IPagedList<FactViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FactGetPagedRequestHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public override async Task<OperationResult<IPagedList<FactViewModel>>> Handle(FactGetPagedRequest request, CancellationToken cancellationToken)
        {
            var operation = OperationResult.CreateResult<IPagedList<FactViewModel>>();

            var items = await _unitOfWork.GetRepository<Fact>()
                .GetPagedListAsync(
                    include: i => i.Include(x => x.Tags),
                    orderBy: o => o.OrderByDescending(x => x.CreatedAt),
                    pageIndex: request.PageIndex,
                    pageSize: request.PageSize,
                    cancellationToken: cancellationToken);

            var mapped = _mapper.Map<IPagedList<FactViewModel>>(items);

            operation.Result = mapped;
            operation.AddSuccess("Success");
            return operation;
        }
    }
}
