using Calabonga.Facts.Web.Data;
using Calabonga.Facts.Web.Infrastructure.Mappers.Base;
using Calabonga.Facts.Web.ViewModels;

namespace Calabonga.Facts.Web.Infrastructure.Mappers
{
    public class TagMapperConfiguration : MapperConfigurationBase
    {
        public TagMapperConfiguration()
        {
            CreateMap<Tag, TagViewModel>();
            CreateMap<Tag, TagUpdateViewModel>();
            CreateMap<TagUpdateViewModel, Tag>()
                .ForMember(x => x.Facts, o => o.Ignore());
        }
    }
}
