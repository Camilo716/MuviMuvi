namespace MuviMuviApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult<List<Actor>>> GetAsync()
    {
        return await _actorService.GetAllActorsAsync();
    }
}
