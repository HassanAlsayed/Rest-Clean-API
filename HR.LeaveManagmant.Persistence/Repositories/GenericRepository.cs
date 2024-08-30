using HR.LeaveManagmant.Application.Contracts.Persistence;
using HR.LeaveManagmant.Domain.Entities.Common;
using HR.LeaveManagmant.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Text.Json;

namespace HR.LeaveManagmant.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly HrDatabaseContext _databaseContext;
    private readonly IConnectionMultiplexer _redis;
    private readonly IDatabaseAsync _database;

    protected GenericRepository(HrDatabaseContext databaseContext, IConnectionMultiplexer redis)
    {
        _databaseContext = databaseContext;
        _redis = redis;
        _database = _redis.GetDatabase();
    }

    public async Task CreateAsync(T entity)
    {
        await _databaseContext.AddAsync(entity);
        await _databaseContext.SaveChangesAsync();
        await _database.StringSetAsync(entity.Id.ToString(), JsonSerializer.Serialize(entity), TimeSpan.FromMinutes(1));
    }

    public async Task DeleteAsync(Guid id)
    {
        var cachedEntity = await _database.StringGetAsync(id.ToString());

        if (!string.IsNullOrEmpty(cachedEntity))
        {
            await _database.KeyDeleteAsync(id.ToString());
        }
        var Leave = await _databaseContext.Set<T>().FindAsync(id);
        _databaseContext.Remove(Leave);
        await _databaseContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        var cachedLeaveTypes = await _database.StringGetAsync("leaveTypes");
        if (!cachedLeaveTypes.IsNullOrEmpty)
        {
            return JsonSerializer.Deserialize<IReadOnlyList<T>>(cachedLeaveTypes!)!;
        }
        var leaveTypes = await _databaseContext.Set<T>().AsNoTracking().ToListAsync();
        await _database.StringSetAsync("leaveTypes", JsonSerializer.Serialize(cachedLeaveTypes), TimeSpan.FromMinutes(1));
        return leaveTypes;
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        var cachedEntity = await _database.StringGetAsync(id.ToString());

        if (!string.IsNullOrEmpty(cachedEntity))
        {
            return JsonSerializer.Deserialize<T>(cachedEntity!)!;
        }
        var entity = await _databaseContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (entity is not null)
        {
            await _database.StringSetAsync(id.ToString(), JsonSerializer.Serialize<T>(entity), TimeSpan.FromMinutes(1));
        }
        return entity!;
    }

    public async Task UpdateAsync(Guid id, T entity)
    {
        var Leave = await _databaseContext.Set<T>().FindAsync(id);

        _databaseContext.Entry(Leave).CurrentValues.SetValues(entity);
        await _databaseContext.SaveChangesAsync();

        await _database.StringSetAsync(id.ToString(), JsonSerializer.Serialize<T>(entity));

    }
}
