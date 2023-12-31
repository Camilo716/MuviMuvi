using MuviMuviApi.Data.Repositories;
using MuviMuviApi.Models;

namespace MuviMuviApi.Services;

public class GenreService
{
    private readonly IRepository<Genre> _genreRepository;

    public GenreService(IRepository<Genre> genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<List<Genre>> GetAllGenresAsync()
    {
        return await _genreRepository.GetAllAsync();
    }

    public async Task<Genre> GetGenreByIdAsync(int id)
    {
        var genre = await _genreRepository.GetByIdAsync(id);
        if (genre == null)
            throw new KeyNotFoundException($"Genre with id {id} not found");

        return genre;
    }

    public async Task<Genre> PostGenreAsync(Genre genre)
    {
        return await _genreRepository.SaveAsync(genre);
    }

    public async Task<Genre> PutGenreAsync(int id, Genre genre)
    {
        return await _genreRepository.UpdateAsync(id, genre);
    }

    internal async Task DeleteGenreAsync(int id)
    {
        bool genreExist = await _genreRepository.ExistsAsync(id);
        if (!genreExist)
            throw new KeyNotFoundException($"Genre with id {id} not found");

        await _genreRepository.DeleteAsync(id);
    }
}