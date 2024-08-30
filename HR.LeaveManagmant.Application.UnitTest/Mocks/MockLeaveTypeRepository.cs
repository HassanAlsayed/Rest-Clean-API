using HR.LeaveManagmant.Application.Contracts.Persistence;
using HR.LeaveManagmant.Domain.Entities;
using Moq;

namespace HR.LeaveManagmant.Application.UnitTest.Mocks
{
    public class MockLeaveTypeRepository
    {
        public static Mock<ILeaveTypeRepository> GetLeaveType()
        {
            var LeaveTypes = new List<LeaveType>
        {
            new LeaveType
            {
                Id = Guid.Parse("29e4d4e6-af4f-40da-adc9-a6d68304b123"),
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
            mockRepo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => LeaveTypes.FirstOrDefault(x => x.Id == id));
            return mockRepo;
        }
    }
}
