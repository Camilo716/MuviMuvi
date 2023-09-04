using MuviMuviApi.Data.Repositories;
using MuviMuviApi.Models;

namespace MuviMuviApi.Services;

public class GenreService
{
    private readonly IGenreRepository _genreRepository;

    public GenreService(IGenreRepository genreRepository)
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
}