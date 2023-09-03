using Microsoft.EntityFrameworkCore;
using MuviMuviApi.Data.Repositories;
using MuviMuviApi.Models;

namespace MuviMuviApi.Data.EntityFramework;

public class EfGenreRepository: IGenreRepository
{
    private readonly ApplicationDbContext _context;

    public EfGenreRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Genre>> GetAllAsync()
    {
        return await _context.Genres.ToListAsync();
    }

    public async Task<Genre> GetByIdAsync(int id)
    {
        return await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<Genre> SaveAsync(Genre genre)
    {
        var genreEntry = await _context.AddAsync(genre);
        await _context.SaveChangesAsync();
        return genreEntry.Entity;    
    }
}