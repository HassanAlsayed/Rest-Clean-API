using AutoMapper;
using HR.LeaveManagmant.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagmant.Application.Features.LeaveType.Quaries.GetAllLeaveTypes;

public class GetAllLeaveTypesQuaryHandler : IRequestHandler<GetAllLeaveTypesQuary, List<LeaveTypeDto>>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;

    public GetAllLeaveTypesQuaryHandler(ILeaveTypeRepository leaveTypeRepository,IMapper mapper)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }

    public async Task<List<LeaveTypeDto>> Handle(GetAllLeaveTypesQuary request, CancellationToken cancellationToken)
    {
        var LeaveTypes = await _leaveTypeRepository.GetAllAsync();

        var result = _mapper.Map<List<LeaveTypeDto>>(LeaveTypes);

        return result;

    }
}
