using AutoMapper;
using HR.LeaveManagmant.Application.Contracts.Persistence;
using HR.LeaveManagmant.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagmant.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, Guid>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;

    public CreateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }
    public async Task<Guid> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var Validator = new UpdateLeaveTypeValidation();

        var ValidationResult = await Validator.ValidateAsync(request);

        if(!ValidationResult.IsValid)
        {
            throw new BadRequestException("Invalid data");
        }

        var LeaveTypeToCreate = _mapper.Map<Domain.Entities.LeaveType>(request);

        await _leaveTypeRepository.CreateAsync(LeaveTypeToCreate);

        return LeaveTypeToCreate.Id;
    }
}
