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

    public async Task<List<Actor>> GetAllActorsAsync()
    {
        return await _actorRepository.GetAllAsync();
    }

    public async Task<Actor> GetActorByIdAsync(int id)
    {
        var actor = await _actorRepository.GetByIdAsync(id);

        if (actor == null)
            throw new KeyNotFoundException($"Actor with id {id} not found");

        return actor;
    }

    public async Task<Actor> PostActorAsync(Actor actor)
    {
        return await _actorRepository.SaveAsync(actor);
    }

    public async Task<Actor> PutActorAsync(int id, Actor actor)
    {
        return await _actorRepository.UpdateAsync(id, actor);
    }

    // public async Task<Actor> DeleteActorAsync(int id, Actor actor)
    // {
    //     bool exist = await _actorRepository.ExistsAsync(id);

    //     if (!exist)
    //         throw new KeyNotFoundException($"Actor with id {id} not found");

    // }
    
}