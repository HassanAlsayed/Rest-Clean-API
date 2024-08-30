using HR.LeaveManagmant.Application.Contracts.Persistence;
using HR.LeaveManagmant.Domain.Entities;
using Moq;

namespace HR.LeaveManagmant.Application.UnitTest.Mocks
{
    public class MockDeleteLeaveTypeRepository
    {
        public static Mock<ILeaveTypeRepository> DeleteLeaveType()
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
            mockRepo.Setup(x => x.DeleteAsync(It.IsAny<Guid>()))
                .Returns((Guid id) => {
                    var LeaveTypeToDelete = LeaveTypes.FirstOrDefault(x => x.Id == id);

                    if (LeaveTypeToDelete is not null)
                    {
                        LeaveTypes.Remove(LeaveTypeToDelete);
                    }
                    return Task.CompletedTask;
                
                });
           

            return mockRepo;
        }
    }
}
