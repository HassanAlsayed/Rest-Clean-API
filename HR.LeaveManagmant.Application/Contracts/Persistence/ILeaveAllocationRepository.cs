using HR.LeaveManagmant.Domain.Entities;

namespace HR.LeaveManagmant.Application.Contracts.Persistence;

    public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
    {
    Task<LeaveAllocation> GetLeaveAllocationWithDetails(Guid id);
    Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails();
    Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails(string userId);
    Task<bool> AllocationExist(string userId, Guid leaveTypeId, int period);
    Task AddAllocations(List<LeaveAllocation> allocations);
    Task<LeaveAllocation> GetUserallocation(string userId,Guid leaveTypeId);
    }

