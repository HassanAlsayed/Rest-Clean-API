using MediatR;

namespace HR.LeaveManagmant.Application.Features.LeaveType.Quaries.GetLeaveTypeDetails
{
    public record GetLeaveTypeDetailsQuary : IRequest<GetLeaveTypeDetailsDto>
    {
        public Guid Id { get; set; }
    }
}
