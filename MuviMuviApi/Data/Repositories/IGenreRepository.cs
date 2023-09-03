using MuviMuviApi.Models;

namespace MuviMuviApi.Data.Repositories;

public interface IGenreRepository
{
    public Task<List<Genre>> GetAllAsync();
    public Task<Genre> GetByIdAsync(int id);
    public Task<Genre> SaveAsync(Genre genre);
}