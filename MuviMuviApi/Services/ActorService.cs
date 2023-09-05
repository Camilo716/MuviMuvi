using Microsoft.AspNetCore.Mvc;
using MuviMuviApi.Data.Repositories;
using MuviMuviApi.Models;

namespace MuviMuviApi.Services;

public class ActorService
{
    private readonly IActorRepository _actorRepository;

    public ActorService(IActorRepository actorRepository)
    {
        _actorRepository = actorRepository;
    }

    public async Task<ActionResult<List<Actor>>> GetAllActorsAsync()
    {
        return await _actorRepository.GetAll();
    }
}