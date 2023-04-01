using Calabonga.AspNetCore.Controllers;
using Calabonga.AspNetCore.Controllers.Records;
using Calabonga.Facts.Web.ViewModels;
using Calabonga.OperationResults;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.Facts.Web.Infrastructure.Services;
using Calabonga.Microservices.Core.Exceptions;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Calabonga.Facts.Web.Data.Entities;

namespace Calabonga.Facts.Web.Controllers.Facts.Command
{
    /// <summary>
    /// Mediator request for fact update
    /// </summary>
    public record FactUpdateRequest(FactEditViewModel Model) : OperationResultRequestBase<Unit>;

    /// <summary>
    /// Mediator request handler for fact update
    /// </summary>
    public class FactUpdateRequestHandler : OperationResultRequestHandlerBase<FactUpdateRequest, Unit>
    {
        private readonly ITagService _tagService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FactUpdateRequestHandler(ITagService tagService, IUnitOfWork unitOfWork, IMapper mapper)
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
            FactUpdateRequest request,
            CancellationToken cancellationToken)
        {
            var operation = OperationResult.CreateResult<Unit>();

            var repository = _unitOfWork.GetRepository<Fact>();
            var fact = await repository.GetFirstOrDefaultAsync(
                predicate: x => x.Id == request.Model.Id,
                include: i => i.Include(x => x.Tags),
                disableTracking: false);

            if (fact is null)
            {
                operation.AddError(new MicroserviceNotFoundException($"Fact with Id {request.Model.Id} not found"));
                return operation;
            }

            // mapping changes from ViewModel
            _mapper.Map(request.Model, fact);

            // processing tag from ViewModel

            repository.Update(fact);

            await _tagService.ProcessTagsAsync(request.Model, fact, cancellationToken);

            await _unitOfWork.SaveChangesAsync();
            if (_unitOfWork.LastSaveChangesResult.IsOk)
            {
                operation.AddSuccess($"Fact {request.Model.Id} successfully updated");
                operation.Result = Unit.Value;
                return operation;
            }

            operation.AddError(_unitOfWork.LastSaveChangesResult.Exception);
            return operation;

        }
    }
}
