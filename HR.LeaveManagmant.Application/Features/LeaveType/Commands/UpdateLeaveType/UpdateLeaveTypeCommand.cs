using MediatR;

namespace HR.LeaveManagmant.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public record UpdateLeaveTypeCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int DefaultDays { get; set; }
    }
}
