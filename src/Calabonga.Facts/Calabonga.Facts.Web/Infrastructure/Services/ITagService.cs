using Calabonga.Facts.Web.Data;
using Calabonga.Facts.Web.Infrastructure.Helpers;
using Calabonga.Facts.Web.ViewModels;
using Calabonga.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Calabonga.Facts.Web.Infrastructure.Services
{
    public interface ITagService
    {
        /// <summary>
        /// Generates tags as cloud
        /// </summary>
        /// <returns></returns>
        Task<List<TagCloud>> GetCloudAsync();

        /// <summary>
        /// Tags processing
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="fact"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task ProcessTagsAsync(IHaveTags viewModel, Fact fact, CancellationToken cancellationToken);
    }

    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        /// <summary>
        /// Generates tags as cloud
        /// </summary>
        /// <returns></returns>
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

            return FactHelper.Generate(tags, 10);
        }

        /// <summary>
        /// Tags processing
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="fact"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task ProcessTagsAsync(IHaveTags viewModel, Fact fact, CancellationToken cancellationToken)
        {
            if (viewModel?.Tags is null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            if (fact == null)
            {
                throw new ArgumentNullException(nameof(fact));
            }

            fact.Tags ??= new Collection<Tag>();

            var tagRepository = _unitOfWork.GetRepository<Tag>();

            var afterEdit = viewModel.Tags!.ToArray();
            var oldArray = tagRepository
                           .GetAll(
                               x => x.Name.ToLower(),
                               x => x.Facts!.Select(p => p.Id).Contains(fact.Id),
                               null)
                           .ToArray();

            var (toCreate, toDelete) = new FactHelper().FindDifference(oldArray, afterEdit);

            if (toDelete.Any())
            {
                foreach (var name in toDelete)
                {
                    var tag = await tagRepository.GetFirstOrDefaultAsync(predicate: x => x.Name.ToLower() == name, disableTracking: false);
                    if (tag == null)
                    {
                        continue;
                    }

                    fact.Tags!.Remove(tag);

                    var used = _unitOfWork.GetRepository<Fact>()
                                          .GetAll(x => x.Tags!.Select(t => t.Name).Contains(tag.Name), true)
                                          .ToArray();

                    if (used.Length == 1)
                    {
                        tagRepository.Delete(tag);
                    }
                }
            }
           
            

            foreach (var name in toCreate)
            {
                var tag = await tagRepository.GetFirstOrDefaultAsync(predicate: x => x.Name.ToLower() == name, disableTracking: false);
                if (tag == null)
                {
                    var t = new Tag
                    {
                        Name = name.Trim().ToLower()
                    };
                    await tagRepository.InsertAsync(t, cancellationToken);
                    fact.Tags!.Add(t);
                }
                else
                {
                    fact.Tags!.Add(tag);
                }
            }
        }
    }
}
