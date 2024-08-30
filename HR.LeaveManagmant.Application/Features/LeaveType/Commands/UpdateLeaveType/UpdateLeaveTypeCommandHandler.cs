using AutoMapper;
using HR.LeaveManagmant.Application.Contracts.Persistence;
using HR.LeaveManagmant.Application.Exceptions;
using HR.LeaveManagmant.Application.Features.LeaveType.Commands.CreateLeaveType;
using MediatR;

namespace HR.LeaveManagmant.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository,IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var Validator = new UpdateLeaveTypeValidation();

            var ValidationResult = await Validator.ValidateAsync(request);

            if (!ValidationResult.IsValid)
            {
                throw new BadRequestException("Invalid data");
            }
            var LeaveTypeToUpdate = _mapper.Map<Domain.Entities.LeaveType>(request);

            await _leaveTypeRepository.UpdateAsync(request.Id,LeaveTypeToUpdate);

            return Unit.Value;
        }
    }
}
