
using MuviMuviApi.Models;

namespace MuviMuviApi.Data.Repositories;

public interface IActorRepository
{
    Task<List<Actor>> GetAllAsync();
    Task<Actor> GetByIdAsync(int id);
    Task<Actor> SaveAsync(Actor actor);
    Task<bool> ExistsAsync(int id);
    Task<Actor> UpdateAsync(int id, Actor actor);
}