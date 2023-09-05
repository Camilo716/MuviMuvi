using Microsoft.EntityFrameworkCore;
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

    public async Task<List<Actor>> GetAll()
    {
        return await _context.Actors.ToListAsync();
    }
}