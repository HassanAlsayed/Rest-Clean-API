using AutoMapper;
using HR.LeaveManagmant.Application.Contracts.Persistence;
using HR.LeaveManagmant.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagmant.Application.MappingProfile;
using HR.LeaveManagmant.Application.UnitTest.Mocks;
using MediatR;
using Moq;
using Shouldly;

namespace HR.LeaveManagmant.Application.UnitTest.Features.LeaveTypes.Commands;

public class UpdateLeaveTypeCommnadHamdlerTests
{
    private readonly Mock<ILeaveTypeRepository> _mockRepo;
    private readonly IMapper _mapper;

    public UpdateLeaveTypeCommnadHamdlerTests()
    {
        _mockRepo = MockUpdateLeaveTypeRepository.UpdateLeaveType();
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<LeaveTypeProfile>();
        });
        _mapper = mapperConfig.CreateMapper();

    }
    [Fact]
    public async Task UpdateLeaveTypeTest()
    {
        var handler = new UpdateLeaveTypeCommandHandler(_mockRepo.Object,_mapper);

        var command = new UpdateLeaveTypeCommand
        {
            Name = "Update Test",
            DefaultDays = 22,
        };
        command.Id = Guid.Parse("29e4d4e6-af4f-40da-adc9-a6d68304b123");
        var result = await handler.Handle(command, CancellationToken.None);
        result.ShouldBeOfType<Unit>();
        result.ShouldBe(Unit.Value);
    }
}
