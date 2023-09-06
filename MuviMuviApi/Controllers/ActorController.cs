namespace MuviMuviApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MuviMuviApi.DTOs;
using MuviMuviApi.Models;
using MuviMuviApi.Services;

[ApiController]
[Route("api/[controller]")]
public class ActorController : ControllerBase
{
    private readonly ActorService _actorService;
    private readonly IMapper _mapper;

    public ActorController(ActorService actorService, IMapper mapper)
    {
        _actorService = actorService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<ActorDTO>>> GetAsync()
    {
        List<Actor> actors =  await _actorService.GetAllActorsAsync();
        List<ActorDTO> actorsDto = _mapper.Map<List<ActorDTO>>(actors);
        return Ok(actorsDto);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ActorDTO>> GetByIdAsync(int id)
    {
        try
        {
            Actor actor = await _actorService.GetActorByIdAsync(id);
            ActorDTO actorDto = _mapper.Map<ActorDTO>(actor);
            return Ok(actorDto);
        }
        catch (KeyNotFoundException keyNotFoundEx)
        {
            return NotFound(keyNotFoundEx.Message);
        }
    }
}
