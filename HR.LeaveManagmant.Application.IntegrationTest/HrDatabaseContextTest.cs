using HR.LeaveManagmant.Domain.Entities;
using HR.LeaveManagmant.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace HR.LeaveManagmant.Application.IntegrationTest;

public class HrDatabaseContextTest
{
    private readonly HrDatabaseContext _dbContext;

    public HrDatabaseContextTest()
    {
        var dbOptions = new DbContextOptionsBuilder<HrDatabaseContext>()
            .UseInMemoryDatabase("Integration Test").Options;
        _dbContext = new HrDatabaseContext(dbOptions);
    }

    [Fact]
    public async Task Save_SetDateCreatedValue()
    {
        // Arrang
        var LeaveType = new LeaveType
        {
            Id = Guid.NewGuid(),
            Name = "Test Vacation",
            DefaultDays = 1
        };

        // Act
         await _dbContext.LeaveTypes.AddAsync(LeaveType);
         await _dbContext.SaveChangesAsync();

        // Assert
        LeaveType.CreatedDate.ShouldNotBeNull();
    }

    [Fact]
    public async Task Save_SetDateModifiedValue()
    {
        // Arrang
        var LeaveType = new LeaveType
        {
            Id = Guid.NewGuid(),
            Name = "Test Vacation",
            DefaultDays = 1
        };

        // Act
        await _dbContext.LeaveTypes.AddAsync(LeaveType);
        await _dbContext.SaveChangesAsync();

        // Assert
        LeaveType.ModifiedDate.ShouldNotBeNull();
    }
}
