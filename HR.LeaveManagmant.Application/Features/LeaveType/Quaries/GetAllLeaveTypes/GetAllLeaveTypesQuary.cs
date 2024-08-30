using MediatR;

namespace HR.LeaveManagmant.Application.Features.LeaveType.Quaries.GetAllLeaveTypes
{
    public class GetAllLeaveTypesQuary : IRequest<List<LeaveTypeDto>>    
    {
    }
}
