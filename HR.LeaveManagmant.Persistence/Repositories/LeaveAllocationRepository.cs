using HR.LeaveManagmant.Application.Contracts.Persistence;
using HR.LeaveManagmant.Domain.Entities;
using HR.LeaveManagmant.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace HR.LeaveManagmant.Persistence.Repositories;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    public LeaveAllocationRepository(HrDatabaseContext databaseContext, IConnectionMultiplexer redis) : base(databaseContext, redis)
    {

    }

    public async Task AddAllocations(List<LeaveAllocation> allocations)
    {
        await _databaseContext.AddRangeAsync(allocations);
        await _databaseContext.SaveChangesAsync();
    }

    public async Task<bool> AllocationExist(string userId, Guid leaveTypeId, int period)
    {
        return await _databaseContext.LeaveAllocations.AnyAsync(x => x.EmployeeId == userId && x.LeaveTypeId == leaveTypeId
         && x.Period == period);
    }

    public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(Guid id)
    {
       return await _databaseContext.LeaveAllocations.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails()
    {
        return await _databaseContext.LeaveAllocations.Include(x => x.LeaveType).AsNoTracking().ToListAsync();
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails(string userId)
    {
        return await _databaseContext.LeaveAllocations.Include(x => x.LeaveType).AsNoTracking().Where(x => x.EmployeeId == userId).ToListAsync();
    }

    public async Task<LeaveAllocation> GetUserallocation(string userId, Guid leaveTypeId)
    {
        return await _databaseContext.LeaveAllocations.Include(x => x.LeaveType).AsNoTracking().FirstOrDefaultAsync(x => x.EmployeeId == userId && x.LeaveTypeId == leaveTypeId);
    }
}
