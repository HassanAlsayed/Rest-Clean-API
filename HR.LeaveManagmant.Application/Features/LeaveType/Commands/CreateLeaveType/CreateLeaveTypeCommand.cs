using HR.LeaveManagmant.Domain.Entities.Common;
using MediatR;

namespace HR.LeaveManagmant.Application.Features.LeaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public int DefaultDays { get; set; }
    }
}
