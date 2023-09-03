using Microsoft.AspNetCore.Mvc;
using MuviMuviApi.Data.Repositories;

namespace MuviMuviApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenreController : ControllerBase
{
    private readonly IGenreRepository _genreRepository;

    public GenreController(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    // [HttpGet]
    // public async Task<ActionResult<string>> GetGenresAsync(string value)
    // {
    // }
}