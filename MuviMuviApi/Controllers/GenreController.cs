using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MuviMuviApi.Data.Repositories;
using MuviMuviApi.Models;
using MuviMuviApi.Services;

namespace MuviMuviApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenreController : ControllerBase
{
    private readonly GenreService _genreService;
    private readonly IMapper _mapper;

    public GenreController(GenreService genreService, IMapper mapper)
    {
        _genreService = genreService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<GenreDTO>>> GetAllsAsync()
    {
        var genres = await _genreService.GetAllGenresAsync();
        var genresDto = _mapper.Map<List<GenreDTO>>(genres);
        return genresDto;
    }

    [HttpGet("{id:int}", Name = "GetById")]
    public async Task<ActionResult<GenreDTO>> GetByIdAsync([FromRoute] int id)
    {
        try
        {
            var genre = await _genreService.GetGenreByIdAsync(id);
            var genreDto = _mapper.Map<GenreDTO>(genre);
            return genreDto;
        }
        catch (KeyNotFoundException keyNotFoundEx)
        {
            return NotFound(keyNotFoundEx.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<GenreCreationDTO>> PostAsync([FromBody] GenreCreationDTO genreDto)
    {
        Genre genre = _mapper.Map<Genre>(genreDto);
        Genre genrePosted = await _genreService.PostGenreAsync(genre);
        GenreDTO genreResponse = _mapper.Map<GenreDTO>(genrePosted);
        return CreatedAtRoute("GetById", new { id = genreResponse.Id}, genreResponse);
    }
}