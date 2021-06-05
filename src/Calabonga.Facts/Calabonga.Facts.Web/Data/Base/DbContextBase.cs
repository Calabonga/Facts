using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Calabonga.EntityFrameworkCore.Entities.Base;
using Calabonga.UnitOfWork;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Calabonga.Facts.Web.Data.Base
{
    public abstract class DbContextBase : IdentityDbContext
    {
        public SaveChangesResult SaveChangesResult { get; set; }

        protected DbContextBase(DbContextOptions options)
            : base(options)
            => SaveChangesResult = new SaveChangesResult();

        /// <summary>
        /// Configures the schema needed for the identity framework.
        /// </summary>
        /// <param name="builder">
        /// The builder being used to construct the model for this context.
        /// </param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(Startup).Assembly);
            base.OnModelCreating(builder);
        }

        /// <summary>
        ///     <para>
        ///         Saves all changes made in this context to the database.
        ///     </para>
        ///     <para>
        ///         This method will automatically call <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges" /> to discover any
        ///         changes to entity instances before saving to the underlying database. This can be disabled via
        ///         <see cref="P:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled" />.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     The number of state entries written to the database.
        /// </returns>
        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateException">
        ///     An error is encountered while saving to the database.
        /// </exception>
        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException">
        ///     A concurrency violation is encountered while saving to the database.
        ///     A concurrency violation occurs when an unexpected number of rows are affected during save.
        ///     This is usually because the data in the database has been modified since it was loaded into memory.
        /// </exception>
        public override int SaveChanges()
        {
            DbSaveChanges();
            return base.SaveChanges();
        }

        /// <summary>
        ///     <para>
        ///         Saves all changes made in this context to the database.
        ///     </para>
        ///     <para>
        ///         This method will automatically call <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges" /> to discover any
        ///         changes to entity instances before saving to the underlying database. This can be disabled via
        ///         <see cref="P:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled" />.
        ///     </para>
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">
        ///     Indicates whether <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges" /> is called after the changes have
        ///     been sent successfully to the database.
        /// </param>
        /// <returns>
        ///     The number of state entries written to the database.
        /// </returns>
        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateException">
        ///     An error is encountered while saving to the database.
        /// </exception>
        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException">
        ///     A concurrency violation is encountered while saving to the database.
        ///     A concurrency violation occurs when an unexpected number of rows are affected during save.
        ///     This is usually because the data in the database has been modified since it was loaded into memory.
        /// </exception>
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            DbSaveChanges();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        /// <summary>
        ///     <para>
        ///         Saves all changes made in this context to the database.
        ///     </para>
        ///     <para>
        ///         This method will automatically call <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges" /> to discover any
        ///         changes to entity instances before saving to the underlying database. This can be disabled via
        ///         <see cref="P:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled" />.
        ///     </para>
        ///     <para>
        ///         Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///         that any asynchronous operations have completed before calling another method on this context.
        ///     </para>
        /// </summary>
        /// <param name="cancellationToken"> A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete. </param>
        /// <returns>
        ///     A task that represents the asynchronous save operation. The task result contains the
        ///     number of state entries written to the database.
        /// </returns>
        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateException">
        ///     An error is encountered while saving to the database.
        /// </exception>
        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException">
        ///     A concurrency violation is encountered while saving to the database.
        ///     A concurrency violation occurs when an unexpected number of rows are affected during save.
        ///     This is usually because the data in the database has been modified since it was loaded into memory.
        /// </exception>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            DbSaveChanges();
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        ///     <para>
        ///         Saves all changes made in this context to the database.
        ///     </para>
        ///     <para>
        ///         This method will automatically call <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges" /> to discover any
        ///         changes to entity instances before saving to the underlying database. This can be disabled via
        ///         <see cref="P:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled" />.
        ///     </para>
        ///     <para>
        ///         Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///         that any asynchronous operations have completed before calling another method on this context.
        ///     </para>
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">
        ///     Indicates whether <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges" /> is called after the changes have
        ///     been sent successfully to the database.
        /// </param>
        /// <param name="cancellationToken"> A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete. </param>
        /// <returns>
        ///     A task that represents the asynchronous save operation. The task result contains the
        ///     number of state entries written to the database.
        /// </returns>
        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateException">
        ///     An error is encountered while saving to the database.
        /// </exception>
        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException">
        ///     A concurrency violation is encountered while saving to the database.
        ///     A concurrency violation occurs when an unexpected number of rows are affected during save.
        ///     This is usually because the data in the database has been modified since it was loaded into memory.
        /// </exception>
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new())
        {
            DbSaveChanges();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void DbSaveChanges()
        {
            // Added

            const string defaultUser = "System";
            var defaultDate = DateTime.UtcNow;

            var addedEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);

            foreach (var entry in addedEntities)
            {
                if (entry.Entity is not IAuditable)
                {
                    return;
                }

                var createdBy = entry.Property(nameof(IAuditable.CreatedBy)).CurrentValue;
                var updatedBy = entry.Property(nameof(IAuditable.UpdatedBy)).CurrentValue;
                var createdAt = entry.Property(nameof(IAuditable.CreatedAt)).CurrentValue;
                var updatedAt = entry.Property(nameof(IAuditable.UpdatedAt)).CurrentValue;


                if (string.IsNullOrEmpty(createdBy?.ToString()))
                {
                    entry.Property(nameof(IAuditable.CreatedBy)).CurrentValue = defaultUser;
                }

                if (string.IsNullOrEmpty(updatedBy?.ToString()))
                {
                    entry.Property(nameof(IAuditable.UpdatedBy)).CurrentValue = defaultUser;
                }

                if (DateTime.Parse(createdAt?.ToString()!).Year < 1970)
                {
                    entry.Property(nameof(IAuditable.CreatedAt)).CurrentValue = defaultDate;
                }

                if (updatedAt != null && DateTime.Parse(updatedAt.ToString()!).Year < 1970)
                {
                    entry.Property(nameof(IAuditable.UpdatedAt)).CurrentValue = defaultDate;
                }
                else
                {
                    entry.Property(nameof(IAuditable.UpdatedAt)).CurrentValue = defaultDate;
                }

                SaveChangesResult.AddMessage("Some entities were created");
            }

            // Modified

            var modifiedEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);

            foreach (var entry in modifiedEntities)
            {
                if (entry.Entity is IAuditable)
                {
                    var userName = entry.Property(nameof(IAuditable.UpdatedBy)).CurrentValue == null
                        ? defaultUser
                        : entry.Property(nameof(IAuditable.UpdatedBy)).CurrentValue;

                    entry.Property(nameof(IAuditable.UpdatedAt)).CurrentValue = DateTime.UtcNow;
                    entry.Property(nameof(IAuditable.UpdatedBy)).CurrentValue = userName;
                }

                SaveChangesResult.AddMessage("Some entities were modified");
            }
        }
    }
}