using AutoMapper;
using HR.LeaveManagmant.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagmant.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagmant.Application.Features.LeaveType.Quaries.GetAllLeaveTypes;
using HR.LeaveManagmant.Application.Features.LeaveType.Quaries.GetLeaveTypeDetails;
using HR.LeaveManagmant.Domain.Entities;

namespace HR.LeaveManagmant.Application.MappingProfile;

public class LeaveTypeProfile : Profile
{
    public LeaveTypeProfile()
    {
        CreateMap<LeaveTypeDto,LeaveType>().ReverseMap();
        CreateMap<GetLeaveTypeDetailsDto, LeaveType>().ReverseMap();
        CreateMap<CreateLeaveTypeCommand,LeaveType>().ReverseMap();
        CreateMap<UpdateLeaveTypeCommand,LeaveType>().ReverseMap();
    }
}
