using HR.LeaveManagmant.Application.Contracts.Persistence;
using HR.LeaveManagmant.Domain.Entities;
using Moq;

namespace HR.LeaveManagmant.Application.UnitTest.Mocks
{
    public class MockUpdateLeaveTypeRepository
    {
        public static Mock<ILeaveTypeRepository> UpdateLeaveType()
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

            mockRepo.Setup(x => x.UpdateAsync(It.IsAny<Guid>(),It.IsAny<LeaveType>()))
                .Returns((Guid id,LeaveType leaveType) =>
                {
                   var existingLeaveType = LeaveTypes.FirstOrDefault(x => x.Id == id);

                    if(existingLeaveType is not null)
                    {
                        existingLeaveType = leaveType;
                    }
                    return Task.CompletedTask;
                });

            return mockRepo;
        }
    }
}
