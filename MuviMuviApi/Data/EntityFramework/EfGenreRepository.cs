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

    public async Task<List<Genre>> GetAllGenresAsync()
    {
        return await _context.Genres.ToListAsync();
    }
}