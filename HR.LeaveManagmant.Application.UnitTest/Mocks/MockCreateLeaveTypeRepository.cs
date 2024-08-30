using HR.LeaveManagmant.Application.Contracts.Persistence;
using HR.LeaveManagmant.Domain.Entities;
using Moq;

namespace HR.LeaveManagmant.Application.UnitTest.Mocks
{
    public class MockCreateLeaveTypeRepository
    {
        public static Mock<ILeaveTypeRepository> CreateLeaveType()
        {
            var LeaveTypes = new List<LeaveType>
        {
            new LeaveType
            {
                Id = Guid.NewGuid(),
                DefaultDays = 10,
                Name = "Test Vacation"
            },
            new LeaveType
            {
                Id = Guid.NewGuid(),
                DefaultDays = 15,
                Name = "Test Sick"
            },
            new LeaveType
            {
                Id = Guid.NewGuid(),
                DefaultDays = 5,
                Name = "Test Maternity"
            }
        };
            var mockRepo = new Mock<ILeaveTypeRepository>();
            mockRepo.Setup(x => x.CreateAsync(It.IsAny<LeaveType>()))
                .Returns((LeaveType leaveType) =>
                {
                    leaveType.Id = Guid.NewGuid();
                    LeaveTypes.Add(leaveType);
                    return Task.CompletedTask;
                });
           

            return mockRepo;
        }
    }
}
