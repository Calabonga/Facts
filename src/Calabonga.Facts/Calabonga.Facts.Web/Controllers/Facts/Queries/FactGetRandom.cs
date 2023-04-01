using AutoMapper;
using Calabonga.AspNetCore.Controllers;
using Calabonga.AspNetCore.Controllers.Records;
using Calabonga.Facts.Web.ViewModels;
using Calabonga.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Calabonga.Facts.Contracts;
using Calabonga.Facts.Web.Data.Entities;

namespace Calabonga.Facts.Web.Controllers.Facts.Queries
{
    public record FactGetRandomRequest : RequestBase<FactViewModel>;

    public class FactGetRandomRequestHandler : RequestHandlerBase<FactGetRandomRequest, FactViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FactGetRandomRequestHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <summary>Handles a request</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        public override async Task<FactViewModel> Handle(FactGetRandomRequest request, CancellationToken cancellationToken)
        {
            var fact = await _unitOfWork.GetRepository<Fact>()
                .GetAll(true)
                .OrderBy(x => Guid.NewGuid())
                .FirstOrDefaultAsync(cancellationToken);

            return fact == null 
                ? new FactViewModel {Content = "No data"}
                : _mapper.Map<FactViewModel>(fact);
        }
    }
}
