using Calabonga.Facts.Web.Data;
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

    public class TagCloudHelper
    {
        public static List<TagCloud> Generate(List<TagCloud> items, int clusterCount)
        {
            var totalCount = items.Count;
            var tagsCloud = items.OrderBy(x => x.Total).ToList();

            var clusters = new List<List<TagCloud>>();
            if (totalCount > 0)
            {
                var min = tagsCloud.Min(c => c.Total);
                var max = tagsCloud.Max(c => c.Total) + min;
                var completeRange = max - min;
                var groupRange = completeRange / (double)clusterCount;
                var cluster = new List<TagCloud>();
                var currentRange = min + groupRange;
                for (var i = 0; i < totalCount; i++)
                {
                    while (tagsCloud.ToArray()[i].Total > currentRange)
                    {
                        clusters.Add(cluster);
                        cluster = new List<TagCloud>();
                        currentRange += groupRange;
                    }
                    cluster.Add(tagsCloud.ToArray()[i]);
                }
                clusters.Add(cluster);
            }
            var result = new List<TagCloud>();
            for (var i = 0; i < clusters.Count; i++)
            {
                result.AddRange(clusters[i].Select(item => new TagCloud
                {
                    Id = item.Id,
                    Name = item.Name,
                    CssClass = "tag" + i,
                    Total = item.Total
                }));
            }

            return result.OrderBy(x => x.Name).ToList();
        }
    }
}