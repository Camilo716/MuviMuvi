
using MuviMuviApi.Models;

namespace MuviMuviApi.Data.Repositories;

public interface IActorRepository
{
    Task<List<Actor>> GetAll();
}