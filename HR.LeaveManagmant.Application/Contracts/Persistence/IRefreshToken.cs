using HR.LeaveManagmant.Domain.Entities.Account;

namespace HR.LeaveManagmant.Application.Contracts.Persistence;

public interface IRefreshToken
{
    public Task SaveRefreshToken(RefreshToken refreshToken);
    public Task<RefreshToken> StoredToken(string tokenHave);
    public Task UpdateStoredToken(RefreshToken refreshToken);
}
