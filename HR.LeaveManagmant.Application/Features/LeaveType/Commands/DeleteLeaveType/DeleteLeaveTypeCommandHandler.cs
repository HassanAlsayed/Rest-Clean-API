using HR.LeaveManagmant.Application.Contracts.Persistence;
using HR.LeaveManagmant.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagmant.Application.Features.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
    }

        public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
          await _leaveTypeRepository.DeleteAsync(request.Id);

          return Unit.Value;
    }
}
