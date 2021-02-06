using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Calabonga.Facts.Web.Data.Configurations
{
    public class NotificationModelConfiguration : IEntityTypeConfiguration<Notification>
    {
        /// <summary>
        ///     Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder"> The builder to be used to configure the entity type. </param>
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id);
            builder.Property(x => x.Content).HasMaxLength(3000).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(50);
            builder.Property(x => x.UpdatedAt);
            builder.Property(x => x.UpdatedBy).HasMaxLength(50);

            builder.Property(x => x.Subject).HasMaxLength(256).IsRequired();
            builder.Property(x => x.Content).HasMaxLength(3000).IsRequired();
            builder.Property(x => x.AddressFrom).HasMaxLength(128).IsRequired();
            builder.Property(x => x.AddressTo).HasMaxLength(128).IsRequired();
            builder.Property(x => x.IsCompleted);

            builder.HasIndex(x => x.AddressFrom);
            builder.HasIndex(x => x.AddressTo);
            builder.HasIndex(x => x.Subject);
            builder.HasIndex(x => x.Content);
        }
    }
}
