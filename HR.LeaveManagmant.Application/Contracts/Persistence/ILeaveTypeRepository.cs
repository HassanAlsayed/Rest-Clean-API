using HR.LeaveManagmant.Domain.Entities;

namespace HR.LeaveManagmant.Application.Contracts.Persistence;

    public interface ILeaveTypeRepository : IGenericRepository<LeaveType>
    {
      Task<bool> IsLeaveTypeUnique(string name);
    }

