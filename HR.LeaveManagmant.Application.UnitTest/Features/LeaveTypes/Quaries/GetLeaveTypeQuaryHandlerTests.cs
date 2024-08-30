using AutoMapper;
using HR.LeaveManagmant.Application.Contracts.Persistence;
using HR.LeaveManagmant.Application.Features.LeaveType.Quaries.GetLeaveTypeDetails;
using HR.LeaveManagmant.Application.MappingProfile;
using HR.LeaveManagmant.Application.UnitTest.Mocks;
using Moq;
using Shouldly;

namespace HR.LeaveManagmant.Application.UnitTest.Features.LeaveTypes.Quaries;

public class GetLeaveTypeQuaryHandlerTests
{
    private readonly Mock<ILeaveTypeRepository> _mockRepo;
    private readonly IMapper _mapper;

    public GetLeaveTypeQuaryHandlerTests()
    {
        _mockRepo = MockLeaveTypeRepository.GetLeaveType();
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<LeaveTypeProfile>();
        });
        _mapper = mapperConfig.CreateMapper();
    }
    [Fact]
    public async Task GetLeaveTypeTest()
    {
        var handler = new GetLeaveTypeDetailsQuaryHandler(_mockRepo.Object, _mapper);

        var leaveTypeId = Guid.Parse("29e4d4e6-af4f-40da-adc9-a6d68304b123");
        var result = await handler.Handle(new GetLeaveTypeDetailsQuary { Id = leaveTypeId }, CancellationToken.None);

        result.ShouldBeOfType<GetLeaveTypeDetailsDto>();
        result.Id.ShouldBe(leaveTypeId);
        result.Name.ShouldBe("Test Vacation");
        result.DefaultDays.ShouldBe(10);
    }
}
