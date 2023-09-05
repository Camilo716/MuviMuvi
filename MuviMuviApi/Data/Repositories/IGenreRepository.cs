using MuviMuviApi.Models;

namespace MuviMuviApi.Data.Repositories;

public interface IGenreRepository
{
    Task<List<Genre>> GetAllAsync();
    Task<Genre> GetByIdAsync(int id);
    Task<Genre> SaveAsync(Genre genre);
    Task<Genre> UpdateAsync(int id, Genre genre);
    Task DeleteAsync(int id);
}