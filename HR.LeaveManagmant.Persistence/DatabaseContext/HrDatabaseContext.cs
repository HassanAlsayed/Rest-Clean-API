using HR.LeaveManagmant.Domain.Entities;
using HR.LeaveManagmant.Domain.Entities.Account;
using HR.LeaveManagmant.Domain.Entities.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace HR.LeaveManagmant.Persistence.DatabaseContext;

public class HrDatabaseContext : IdentityDbContext
{
    public HrDatabaseContext(DbContextOptions<HrDatabaseContext> options) : base(options) { }
   
    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }
    public DbSet<RefreshToken> RefreshToken { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // add this for all configuration
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HrDatabaseContext).Assembly);

        // we can add this also but for each configuration we should write this
       // modelBuilder.ApplyConfiguration(new LeaveTypeConfiguration());
        base.OnModelCreating(modelBuilder);
    }

    // since i dont to update the time manually 
   public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
{
    foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
        .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
    {
            entry.Entity.ModifiedDate = DateTime.UtcNow;
            entry.Entity.CreatedDate = DateTime.UtcNow;

            if (entry.State == EntityState.Added)
            {
              entry.Entity.CreatedDate = DateTime.UtcNow;
            }
    }

    return base.SaveChangesAsync(cancellationToken);
}

}
