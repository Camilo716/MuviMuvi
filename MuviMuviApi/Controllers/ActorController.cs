namespace MuviMuviApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
}
