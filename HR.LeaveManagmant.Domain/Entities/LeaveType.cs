using HR.LeaveManagmant.Domain.Entities.Common;

namespace HR.LeaveManagmant.Domain.Entities;

public class LeaveType : BaseEntity
{
    public string Name { get; set; }
    public int DefaultDays { get; set; }
}
