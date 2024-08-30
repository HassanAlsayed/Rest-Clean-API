namespace HR.LeaveManagmant.Application.Features.LeaveType.Quaries.GetLeaveTypeDetails
{
    public class GetLeaveTypeDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int DefaultDays { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get;set; }

    }
}
