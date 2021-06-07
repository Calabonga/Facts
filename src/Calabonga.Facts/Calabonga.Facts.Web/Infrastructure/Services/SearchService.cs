using Calabonga.Facts.Contracts;
using Calabonga.Facts.Web.Data;
using Calabonga.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Calabonga.Facts.Web.Infrastructure.Services
{
    public class SearchService : ISearchService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SearchService(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public List<string> SearchTags(string term)
        {
            var items = _unitOfWork.GetRepository<Tag>()
                                   .GetAll(
                                       s => s.Name,
                                       x => x.Name.ToLower().StartsWith(term.ToLower()),
                                       true)
                                   .ToList();

            return items;
        }

        public List<FactViewModel> SearchContent(string term)
        {
            var items = _unitOfWork.GetRepository<Fact>()
                                   .GetAll(predicate: x => x.Content.Contains(term), include: i => i.Include(x => x.Tags))
                                   .Take(10)
                                   .ToList();

            return _mapper.Map<List<FactViewModel>>(items);
        }
    }
}