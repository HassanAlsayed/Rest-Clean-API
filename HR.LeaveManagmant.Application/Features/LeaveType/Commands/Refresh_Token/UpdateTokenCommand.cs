using HR.LeaveManagmant.Domain.Entities.Account;
using MediatR;

namespace HR.LeaveManagmant.Application.Features.LeaveType.Commands.Refresh_Token;

public record UpdateTokenCommand : IRequest<Unit>
{
    public RefreshToken refreshToken { get; set; }

    
}
