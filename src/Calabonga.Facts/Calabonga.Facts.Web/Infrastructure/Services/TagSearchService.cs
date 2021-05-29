using Calabonga.Facts.Contracts;
using Calabonga.Facts.Web.Data;
using Calabonga.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace Calabonga.Facts.Web.Infrastructure.Services
{
    public class TagSearchService : ITagSearchService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagSearchService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

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
    }
}
