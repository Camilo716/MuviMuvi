using Microsoft.EntityFrameworkCore;
using MuviMuviApi.Data.Repositories;
using MuviMuviApi.Models;

namespace MuviMuviApi.Data.EntityFramework;

public class EfRepository<TEntity>: IRepository<TEntity> where TEntity: class, IId
{
    private readonly ApplicationDbContext _context;

    public EfRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity> SaveAsync(TEntity entity)
    {
        var entityEntry = await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task<TEntity> UpdateAsync(int id, TEntity entity)
    {
        var existingEntity = await _context.Set<TEntity>().FindAsync(id);
        if (existingEntity == null)
        {
            return null;
        }

        foreach (var property in _context.Entry(existingEntity).Properties)
        {
            if (property.Metadata.Name != "Id")
            {
                property.CurrentValue = _context.Entry(entity).Property(property.Metadata.Name).CurrentValue;
            }
        }      
        await _context.SaveChangesAsync();
        return existingEntity;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        if (entity != null)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Set<TEntity>().AnyAsync(e => e.Id == id);
    }
}