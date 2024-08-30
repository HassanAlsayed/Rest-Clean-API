using HR.LeaveManagmant.Application.Contracts.Persistence;
using HR.LeaveManagmant.Domain.Entities.Account;
using MediatR;

namespace HR.LeaveManagmant.Application.Features.LeaveType.Commands.Refresh_Token;

public class UpdateTokenCommandHandler : IRequestHandler<UpdateTokenCommand, Unit>
{
    private readonly IRefreshToken _refreshToken;

    public UpdateTokenCommandHandler(IRefreshToken refreshToken)
    {
        _refreshToken = refreshToken;
    }
    public async Task<Unit> Handle(UpdateTokenCommand request, CancellationToken cancellationToken)
    {
         await _refreshToken.UpdateStoredToken(request.refreshToken);
        return Unit.Value;
    }
}
