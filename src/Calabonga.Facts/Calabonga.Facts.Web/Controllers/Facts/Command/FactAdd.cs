using Calabonga.AspNetCore.Controllers;
using Calabonga.AspNetCore.Controllers.Records;
using Calabonga.Facts.Web.ViewModels;
using Calabonga.OperationResults;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.Facts.Web.Data;
using Calabonga.Facts.Web.Infrastructure.Services;
using Calabonga.UnitOfWork;
using MediatR;

namespace Calabonga.Facts.Web.Controllers.Facts.Command
{
    /// <summary>
    /// Mediator request for fact update
    /// </summary>
    public record FactAddRequest(FactCreateViewModel Model) : OperationResultRequestBase<Unit>;

    /// <summary>
    /// Mediator request handler for fact update
    /// </summary>
    public class FactAddRequestHandler : OperationResultRequestHandlerBase<FactAddRequest, Unit>
    {
        private readonly ITagService _tagService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FactAddRequestHandler(ITagService tagService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _tagService = tagService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>Handles a request</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        public override async Task<OperationResult<Unit>> Handle(
            FactAddRequest request,
            CancellationToken cancellationToken)
        {
            var operation = OperationResult.CreateResult<Unit>();

            var repository = _unitOfWork.GetRepository<Fact>();
            var fact = _mapper.Map<Fact>(request.Model);

            // processing tag from ViewModel
            await _tagService.ProcessTagsAsync(request.Model, fact, cancellationToken);

            await repository.InsertAsync(fact, cancellationToken);

            await _unitOfWork.SaveChangesAsync();
            if (_unitOfWork.LastSaveChangesResult.IsOk)
            {
                operation.AddSuccess($"Fact {fact.Id} successfully created");
                operation.Result = Unit.Value;
                return operation;
            }

            operation.AddError(_unitOfWork.LastSaveChangesResult.Exception);
            return operation;

        }
    }
}