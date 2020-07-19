using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Deed> Deeds { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<Purpose> Purposes { get; set; }
        public DbSet<PurposeStatus> PurposeStatuses { get; set; }
        public DbSet<SchedulePeriod> SchedulePeriods { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var EditedEntities = ChangeTracker.Entries()
                .Where(E => E.State == EntityState.Modified)
                .ToList();

            EditedEntities.ForEach(E =>
            {
                E.Property("DateUpdate").CurrentValue = DateTime.UtcNow;
            });

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
