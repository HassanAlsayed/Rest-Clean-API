using MediatR;

namespace HR.LeaveManagmant.Application.Features.LeaveType.Commands.DeleteLeaveType
{
    public record DeleteLeaveTypeCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
