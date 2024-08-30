using MediatR;
using HR.LeaveManagmant.Domain.Entities.Account;
namespace HR.LeaveManagmant.Application.Features.LeaveType.Commands.Refresh_Token;

public class RefreshTokenCommand : IRequest<Unit>
{
    public RefreshToken RefreshToken { get; set; }

    public RefreshTokenCommand(RefreshToken refreshToken)
    {
        RefreshToken = refreshToken;
    }
}
