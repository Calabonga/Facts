using AutoMapper;
using Calabonga.AspNetCore.Controllers.Base;
using Calabonga.Facts.Web.Data;
using Calabonga.Facts.Web.ViewModels;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Calabonga.Facts.Web.Controllers.Facts.Queries
{
    public class FactGetByIdRequest : OperationResultRequestBase<FactViewModel>
    {
        public FactGetByIdRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }

    public class FactGetByIdRequestHandler : OperationResultRequestHandlerBase<FactGetByIdRequest, FactViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FactGetByIdRequestHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public override async Task<OperationResult<FactViewModel>> Handle(FactGetByIdRequest request, CancellationToken cancellationToken)
        {
            var operation = OperationResult.CreateResult<FactViewModel>();
            operation.AppendLog("Searching fact in DB");
            var entity = await _unitOfWork.GetRepository<Fact>()
                .GetFirstOrDefaultAsync(
                    predicate: x => x.Id == request.Id,
                    include: i => i.Include(x => x.Tags));

            if (entity is null)
            {
                operation.AddWarning($"Факт с идентификтором {request.Id} не найден.");
                return operation;
            }

            operation.AppendLog("Fact found. Mapping...");
            operation.Result = _mapper.Map<FactViewModel>(entity);
            operation.AppendLog("Return fact to UI");
            return operation;
        }
    }
}
