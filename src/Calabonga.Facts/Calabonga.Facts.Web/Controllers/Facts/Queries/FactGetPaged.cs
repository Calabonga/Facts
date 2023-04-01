using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.AspNetCore.Controllers;
using Calabonga.AspNetCore.Controllers.Base;
using Calabonga.Facts.Web.ViewModels;
using Calabonga.OperationResults;
using Calabonga.PredicatesBuilder;
using Calabonga.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using Calabonga.Facts.Contracts;
using Calabonga.Facts.Web.Data.Entities;

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
            var predicate = BuildPredicate(request);
            var items = await _unitOfWork.GetRepository<Fact>()
                    .GetPagedListAsync(
                        predicate: predicate,
                        include: i => i.Include(x => x.Tags),
                        orderBy: o => o.OrderByDescending(x => x.CreatedAt),
                        pageIndex: request.PageIndex,
                        pageSize: request.PageSize,
                        cancellationToken: cancellationToken);

            var mapped = _mapper.Map<IPagedList<FactViewModel>>(items);

            operation.Result = mapped;
            if (mapped.TotalCount > 0)
            {
                operation.AddSuccess("Success");
            }
            return operation;
        }

        private Expression<Func<Fact, bool>> BuildPredicate(FactGetPagedRequest request)
        {
            var predicate = PredicateBuilder.True<Fact>();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                predicate = predicate.And(x => x.Content.Contains(request.Search));
            }

            if (!string.IsNullOrWhiteSpace(request.Tag))
            {
                predicate = predicate.And(x => x.Tags!.Select(t => t.Name).Contains(request.Tag));
            }

            return predicate;
        }
    }
}