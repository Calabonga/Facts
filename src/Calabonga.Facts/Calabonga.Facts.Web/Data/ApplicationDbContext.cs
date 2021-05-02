using Calabonga.Facts.Web.Data.Base;
using Microsoft.EntityFrameworkCore;

namespace Calabonga.Facts.Web.Data
{
    public class ApplicationDbContext : DbContextBase
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Fact> Facts { get; set; } = null!;

        public DbSet<Tag> Tags { get; set; } = null!;

        public DbSet<Notification> Notifications { get; set; } = null!;
    }
}
