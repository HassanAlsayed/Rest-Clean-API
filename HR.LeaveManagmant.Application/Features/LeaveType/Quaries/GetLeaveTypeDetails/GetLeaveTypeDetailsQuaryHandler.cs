using AutoMapper;
using HR.LeaveManagmant.Application.Contracts.Persistence;
using HR.LeaveManagmant.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagmant.Application.Features.LeaveType.Quaries.GetLeaveTypeDetails;

public class GetLeaveTypeDetailsQuaryHandler : IRequestHandler<GetLeaveTypeDetailsQuary, GetLeaveTypeDetailsDto>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;

    public GetLeaveTypeDetailsQuaryHandler(ILeaveTypeRepository leaveTypeRepository,IMapper mapper)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }

    public async Task<GetLeaveTypeDetailsDto> Handle(GetLeaveTypeDetailsQuary request, CancellationToken cancellationToken)
    {
       var LeaveType = await _leaveTypeRepository.GetByIdAsync(request.Id);

        if (LeaveType is null)
        {
            throw new NotFoundException(nameof(LeaveType), request.Id);
        }

        var result = _mapper.Map<GetLeaveTypeDetailsDto>(LeaveType);

        return result;
    }
}
