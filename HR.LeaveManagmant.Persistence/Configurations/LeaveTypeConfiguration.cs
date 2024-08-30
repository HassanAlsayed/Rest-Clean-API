using HR.LeaveManagmant.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagmant.Persistence.Configurations
{
    public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.HasData(
               new LeaveType
               {
                   Id = Guid.NewGuid(),
                   Name = "Vacation",
                   DefaultDays = 10,
                   CreatedDate = DateTime.Now,
                   ModifiedDate = DateTime.Now,
               }

                );

            // at database level we can also add some condtions

            builder.Property(x => x.Name).IsRequired()
                .HasMaxLength(100);
        }
    }
}
