using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MuviMuviApi.Data.Repositories;
using MuviMuviApi.Models;

namespace MuviMuviApi.Data.EntityFramework;

public class EfActorRepository : IActorRepository
{
    private readonly ApplicationDbContext _context;

    public EfActorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Actor>> GetAllAsync()
    {
        return await _context.Actors.ToListAsync();
    }

    public async Task<Actor> GetByIdAsync(int id)
    {
        return await _context.Actors
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Actor> SaveAsync(Actor actor)
    {
        var actorEntry = await _context.AddAsync(actor); 
        await _context.SaveChangesAsync();
        return actorEntry.Entity;
    }

    public Task<bool> ExistsAsync(int id)
    {
        return _context.Actors.AnyAsync(a => a.Id == id);
    }

    public async Task<Actor> UpdateAsync(int id, Actor actor)
    {
        actor.Id = id;
        _context.Entry(actor).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return actor;
    }

    public async Task DeleteAsync(int id)
    {
        _context.Remove(new Actor { Id = id });
        await _context.SaveChangesAsync();    
    }
}