using Calabonga.AspNetCore.Controllers;
using Calabonga.AspNetCore.Controllers.Base;
using Calabonga.Facts.Web.Data.Entities;
using Calabonga.Facts.Web.ViewModels;
using Calabonga.OperationResults;
using Calabonga.PredicatesBuilder;
using Calabonga.UnitOfWork;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Calabonga.Facts.Web.Controllers.Administrator.Queries
{
    public class NotificationGetPagedRequest : RequestBase<OperationResult<IPagedList<NotificationViewModel>>>
    {
        public NotificationGetPagedRequest(int pageIndex, string search, bool notProcessed)
        {
            Search = search;
            NotProcessed = notProcessed;
            PageIndex = pageIndex > 0 ? pageIndex - 1 : pageIndex;
        }

        public int? PageIndex { get; }

        public bool NotProcessed { get; }

        public string Search { get; }
    }

    public class NotificationGetPagedRequestHandler : OperationResultRequestHandlerBase<NotificationGetPagedRequest, IPagedList<NotificationViewModel>>, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationGetPagedRequestHandler(
            IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public override async Task<OperationResult<IPagedList<NotificationViewModel>>> Handle(NotificationGetPagedRequest request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<Notification>();
            var predicate = BuildPredicate(request);

            var posts = await repository.GetPagedListAsync(
                NotificationSelectors.Default,
                predicate,
                o => o.OrderByDescending(x => x.CreatedAt),
                pageIndex: request.PageIndex ?? 0,
                pageSize: 25,
                cancellationToken: cancellationToken);

            return OperationResult.CreateResult(posts);
        }

        private Expression<Func<Notification, bool>> BuildPredicate(NotificationGetPagedRequest request)
        {
            var predicate = PredicateBuilder.True<Notification>();

            if (request.NotProcessed)
            {
                predicate = predicate.And(x => !x.IsCompleted);
            }

            if (string.IsNullOrWhiteSpace(request.Search))
            {
                return predicate;
            }

            {
                var term = request.Search;
                predicate = predicate.And(x => x.Content != null && x.Content.Contains(term));
                predicate = predicate.Or(x => x.Subject.Contains(term));
            }

            return predicate;
        }

        public void Dispose() => _unitOfWork.Dispose();
    }
}