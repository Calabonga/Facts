using Calabonga.Facts.Web.Data;
using Calabonga.Facts.Web.Infrastructure.Helpers;
using Calabonga.Facts.Web.ViewModels;
using Calabonga.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calabonga.Facts.Web.Infrastructure.Services
{
    public interface ITagService
    {
        Task<List<TagCloud>> GetCloudAsync();
    }

    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<List<TagCloud>> GetCloudAsync()
        {
            var tags = await _unitOfWork.GetRepository<Tag>()
                .GetAll(true)
                .Select(s => new TagCloud
                {
                    Name = s.Name,
                    Id = s.Id,
                    CssClass = "",
                    Total = s.Facts == null ? 0 : s.Facts!.Count
                })
                .ToListAsync();

            return TagCloudHelper.Generate(tags, 10);
        }
    }
}