using HR.LeaveManagmant.Application.Contracts.Persistence;
using HR.LeaveManagmant.Domain.Entities.Account;
using HR.LeaveManagmant.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagmant.Persistence.Repositories;

public class SaveRefreshtoken : IRefreshToken
{
    private readonly HrDatabaseContext _context;

    public SaveRefreshtoken(HrDatabaseContext context)
    {
       _context = context;
    }
     public async Task SaveRefreshToken(RefreshToken refreshToken)
    {
        await _context.RefreshToken.AddAsync(refreshToken);
        await _context.SaveChangesAsync();
    }

    public async Task<RefreshToken> StoredToken(string tokenHave)
    {
        return await _context.RefreshToken.FirstOrDefaultAsync(x => x.Token == tokenHave);
    }
     
    public async Task UpdateStoredToken(RefreshToken refreshToken)
    {
         _context.RefreshToken.Update(refreshToken);
    }
}
