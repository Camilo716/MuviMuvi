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

    [HttpGet("{id:int}", Name = "GetActorById")]
    public async Task<ActionResult<ActorDTO>> GetByIdAsync([FromRoute] int id)
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

    [HttpPost]
    public async Task<ActionResult<ActorDTO>> PostAsync([FromBody] ActorCreationDTO actorCreationDTO)
    {
        Actor actor = _mapper.Map<Actor>(actorCreationDTO);
        Actor actorPosted = await _actorService.PostActorAsync(actor);

        ActorDTO genreResponse = _mapper.Map<ActorDTO>(actorPosted);
        return CreatedAtRoute("GetActorById", new { id = genreResponse.Id}, genreResponse);
    }


    [HttpPut("{id:int}")]
    public async Task<ActionResult<ActorDTO>> PutAsync([FromRoute] int id, [FromBody] ActorCreationDTO actorCreationDTO)
    {
        Actor actor = _mapper.Map<Actor>(actorCreationDTO);
        await _actorService.PutActorAsync(id, actor);

        ActorDTO actorDTO = _mapper.Map<ActorDTO>(actor);
        return Ok(actorDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        try
        {
            await _actorService.DeleteActorAsync(id);
            return NoContent();         
        }
        catch (KeyNotFoundException keyNotFoundEx)
        {
            return NotFound();
        }
    }
}
