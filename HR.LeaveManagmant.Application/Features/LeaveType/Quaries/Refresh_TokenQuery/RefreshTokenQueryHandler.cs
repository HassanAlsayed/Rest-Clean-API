using HR.LeaveManagmant.Application.Contracts.Persistence;
using HR.LeaveManagmant.Domain.Entities.Account;
using MediatR;

namespace HR.LeaveManagmant.Application.Features.LeaveType.Quaries.Refresh_TokenQuery;

public class RefreshTokenQueryHandler : IRequestHandler<RefreshTokenQuery, RefreshToken>
{
    private readonly IRefreshToken _refreshToken;

    public RefreshTokenQueryHandler(IRefreshToken refreshToken)
    {
        _refreshToken = refreshToken;
    }
    public async Task<RefreshToken> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
    {
        return await _refreshToken.StoredToken(request.tokenHave);
    }
}
