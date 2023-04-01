using Calabonga.Facts.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Calabonga.Facts.Web.Data.Main.Configurations
{
    public class TagModelConfiguration : IEntityTypeConfiguration<Tag>
    {
        /// <summary>
        ///     Configures the entity of type
        /// </summary>
        /// <param name="builder"> The builder to be used to configure the entity type. </param>
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tags");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(50);

            builder.HasIndex(x => x.Name);
        }
    }
}
