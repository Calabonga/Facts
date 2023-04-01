using Calabonga.Facts.Web.Data.Entities;
using Calabonga.Facts.Web.Infrastructure.Mappers.Base;
using Calabonga.Facts.Web.Infrastructure.Providers;

namespace Calabonga.Facts.Web.Infrastructure.Mappers
{
    public class NotificationMapperConfiguration : MapperConfigurationBase
    {
        public NotificationMapperConfiguration()
        {
            CreateMap<Notification, EmailMessage>()
                .ForMember(x => x.Author, o => o.MapFrom(x => x.CreatedBy))
                .ForMember(x => x.Recipient, o => o.Ignore())
                .ForMember(x => x.Body, o => o.MapFrom(x => x.Content))
                .ForMember(x => x.IsHtml, o => o.MapFrom(_ => true));
        }
    }
}
