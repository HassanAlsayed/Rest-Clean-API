﻿using HR.LeaveManagmant.Domain.Entities.Common;

namespace HR.LeaveManagmant.Domain.Entities;

public class LeaveAllocation : BaseEntity
{
    public int NumberOfDays { get; set; }

    public LeaveType LeaveType { get; set; }
    public Guid LeaveTypeId { get; set; }

    public int Period  { get; set; }

    public string EmployeeId { get; set; }
}
