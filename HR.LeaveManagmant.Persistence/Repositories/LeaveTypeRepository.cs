using HR.LeaveManagmant.Application.Contracts.Persistence;
using HR.LeaveManagmant.Domain.Entities;
using HR.LeaveManagmant.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace HR.LeaveManagmant.Persistence.Repositories;

public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
{
    public LeaveTypeRepository(HrDatabaseContext databaseContext, IConnectionMultiplexer redis) : base(databaseContext,redis)
    {
       
    }

    public async Task<bool> IsLeaveTypeUnique(string name)
    {
        return await _databaseContext.LeaveTypes.AnyAsync(x => x.Name == name);
    }

}
