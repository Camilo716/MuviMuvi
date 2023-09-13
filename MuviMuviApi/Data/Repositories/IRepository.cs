using MuviMuviApi.Models;

namespace MuviMuviApi.Data.Repositories;

public interface IRepository<TEntity> where TEntity : class, IId
{
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(int id);
    Task<TEntity> SaveAsync(TEntity entity);
    Task<TEntity> UpdateAsync(int id, TEntity entity);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}