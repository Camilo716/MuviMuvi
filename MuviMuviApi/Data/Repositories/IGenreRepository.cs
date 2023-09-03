using MuviMuviApi.Models;

namespace MuviMuviApi.Data.Repositories;

public interface IGenreRepository
{
    public List<Genre> GetRepositories();
}