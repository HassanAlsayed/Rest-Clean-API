using AutoMapper;
using HR.LeaveManagmant.Application.Contracts.Persistence;
using HR.LeaveManagmant.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagmant.Application.Features.LeaveType.Commands.DeleteLeaveType;
using HR.LeaveManagmant.Application.Features.LeaveType.Quaries.GetLeaveTypeDetails;
using HR.LeaveManagmant.Application.MappingProfile;
using HR.LeaveManagmant.Application.UnitTest.Mocks;
using HR.LeaveManagmant.Domain.Entities;
using MediatR;
using Moq;
using Shouldly;

namespace HR.LeaveManagmant.Application.UnitTest.Features.LeaveTypes.Commands;

public class DeleteLeaveTypeCommandHandlerTests
{
    private readonly Mock<ILeaveTypeRepository> _mockRepo;

    public DeleteLeaveTypeCommandHandlerTests()
    {
        _mockRepo = MockDeleteLeaveTypeRepository.DeleteLeaveType();
    }
    [Fact]
    public async Task DeleteLeaveTypeTest()
    {
        var handler = new DeleteLeaveTypeCommandHandler(_mockRepo.Object);

        var id = Guid.Parse("29e4d4e6-af4f-40da-adc9-a6d68304b123");
        var result = await handler.Handle(new DeleteLeaveTypeCommand { Id = id},CancellationToken.None);
        result.ShouldBeOfType<Unit>();
        result.ShouldBe(Unit.Value);

    }
}
