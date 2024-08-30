using AutoMapper;
using HR.LeaveManagmant.Application.Contracts.Persistence;
using HR.LeaveManagmant.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagmant.Application.Features.LeaveType.Quaries.GetLeaveTypeDetails;
using HR.LeaveManagmant.Application.MappingProfile;
using HR.LeaveManagmant.Application.UnitTest.Mocks;
using HR.LeaveManagmant.Domain.Entities;
using Moq;
using Shouldly;

namespace HR.LeaveManagmant.Application.UnitTest.Features.LeaveTypes.Commands;

public class CreateLeaveTypeCommandHandlerTests
{
    private readonly Mock<ILeaveTypeRepository> _mockRepo;
    private readonly IMapper _mapper;

    public CreateLeaveTypeCommandHandlerTests()
    {
        _mockRepo = MockCreateLeaveTypeRepository.CreateLeaveType();
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<LeaveTypeProfile>();
        });
        _mapper = mapperConfig.CreateMapper();
    }
    [Fact]
    public async Task CreateLeaveTypeTest()
    {
        var handler = new CreateLeaveTypeCommandHandler(_mockRepo.Object,_mapper);

        var command = new CreateLeaveTypeCommand
        {
            Name = "Test",
            DefaultDays = 2,
        };
        
        var result = await handler.Handle(command,CancellationToken.None);
        result.ShouldBeOfType<Guid>();
        result.ShouldNotBe(Guid.Empty);

    }
}
