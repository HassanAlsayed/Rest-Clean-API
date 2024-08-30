using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HR.LeaveManagmant.Persistence.DatabaseContext
{
    public class HrDatabaseContextFactory : IDesignTimeDbContextFactory<HrDatabaseContext>
    {
        public HrDatabaseContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("C:\\Users\\user1\\Desktop\\charpProjects\\Rest-Api-Clean\\HR.LeaveManagmant.Api\\appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<HrDatabaseContext>();
            var connectionString = configuration.GetConnectionString("HrDatabaseConnectionString");

            optionsBuilder.UseSqlServer(connectionString);

            return new HrDatabaseContext(optionsBuilder.Options);
        }
    }
}
