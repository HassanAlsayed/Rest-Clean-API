using AutoMapper;
using HR.LeaveManagmant.Application.Contracts.Persistence;
using HR.LeaveManagmant.Application.Features.LeaveType.Quaries.GetAllLeaveTypes;
using HR.LeaveManagmant.Application.MappingProfile;
using HR.LeaveManagmant.Application.UnitTest.Mocks;
using Moq;
using Shouldly;

namespace HR.LeaveManagmant.Application.UnitTest.Features.LeaveTypes.Quaries;

public class GetLeaveTypesListQuaryHandlerTests
{
    private readonly Mock<ILeaveTypeRepository> _mockRepo;
    private readonly IMapper _mapper;

    public GetLeaveTypesListQuaryHandlerTests()
    {
        _mockRepo = MockLeaveTypesRepository.GetLeaveTypes();

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<LeaveTypeProfile>();
        });
        _mapper = mapperConfig.CreateMapper();

    }
    [Fact]
    public async Task GetLeaveTypesTest()
    {
        var handler = new GetAllLeaveTypesQuaryHandler(_mockRepo.Object,_mapper);

        var result = await handler.Handle(new GetAllLeaveTypesQuary(),CancellationToken.None);

        result.ShouldBeOfType<List<LeaveTypeDto>>();
        result.Count.ShouldBe(3);
    }
}
