using HR.LeaveManagmant.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagmant.Application.Features.LeaveType.Commands.Refresh_Token;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Unit>
{
    private readonly IRefreshToken _refreshToken;

    public RefreshTokenCommandHandler(IRefreshToken refreshToken)
    {
        _refreshToken = refreshToken;
    }
    public async Task<Unit> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        await _refreshToken.SaveRefreshToken(request.RefreshToken);
       return Unit.Value;
    }
}
