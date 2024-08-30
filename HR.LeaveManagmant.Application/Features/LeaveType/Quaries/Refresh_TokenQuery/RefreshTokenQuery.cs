using HR.LeaveManagmant.Domain.Entities.Account;
using MediatR;

namespace HR.LeaveManagmant.Application.Features.LeaveType.Quaries.Refresh_TokenQuery;

public class RefreshTokenQuery : IRequest<RefreshToken>
{
    public string tokenHave {  get; set; }
}
