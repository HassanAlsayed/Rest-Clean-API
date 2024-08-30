using HR.LeaveManagmant.Application.Contracts.Persistence;
using HR.LeaveManagmant.Domain.Entities;
using HR.LeaveManagmant.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace HR.LeaveManagmant.Persistence.Repositories;

public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
{
    public LeaveRequestRepository(HrDatabaseContext databaseContext, IConnectionMultiplexer redis) : base(databaseContext, redis)
    {

    }

    public async Task<LeaveRequest> GetLeaveRequestWithDetails(Guid id)
    {
        return await _databaseContext.LeaveRequests.Include(x => x.LeaveType).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestWithDetails()
    {
       return await _databaseContext.LeaveRequests.Include(x => x.LeaveType).AsNoTracking().ToListAsync();
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestWithDetails(string userId)
    {
       return await _databaseContext.LeaveRequests.Where(x => x.RequestingEmployeeId == userId)
            .Include(x => x.LeaveType).AsNoTracking().ToListAsync();
    }
}
