
using MuviMuviApi.Models;

namespace MuviMuviApi.Data.Repositories;

public interface IActorRepository
{
    Task<List<Actor>> GetAllAsync();
    Task<Actor> GetByIdAsync(int id);
    Task<Actor> SaveAsync(Actor actor);
}