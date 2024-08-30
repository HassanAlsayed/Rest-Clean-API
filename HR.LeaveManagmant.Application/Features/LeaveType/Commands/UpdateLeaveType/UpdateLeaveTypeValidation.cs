using FluentValidation;

namespace HR.LeaveManagmant.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeValidation : AbstractValidator<UpdateLeaveTypeCommand>
    {
        public UpdateLeaveTypeValidation()
        {
            RuleFor(x => x.Name)
                 .NotEmpty().WithMessage("{PrpertyName} is required")
                 .NotNull()
                 .MaximumLength(70).WithMessage("{PrpertyName} must me fewer than 70 characters");

            RuleFor(x => x.DefaultDays)
                .LessThan(100).WithMessage("{PrpertyName} cannot exceed 100")
                .GreaterThan(1).WithMessage("{PrpertyName} cannot be less than 1");
        }
    }
}
