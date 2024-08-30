using HR.LeaveManagmant.Domain.Entities.Common;

namespace HR.LeaveManagmant.Application.Contracts.Persistence;

public partial interface IGenericRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid id);
    Task CreateAsync(T entity);  
    Task UpdateAsync(Guid id,T entity);
    Task DeleteAsync(Guid id);

}
