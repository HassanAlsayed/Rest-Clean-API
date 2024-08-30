namespace HR.LeaveManagmant.Application.Features.LeaveType.Quaries.GetAllLeaveTypes
{
    public class LeaveTypeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int DefaultDays { get; set; }
    }
}
