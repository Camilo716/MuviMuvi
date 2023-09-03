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
    public async Task<ActionResult<List<GenreDto>>> GetAllGenresAsync()
    {
        var genres = await _genreService.GetAllGenresAsync();
        var genresDto = _mapper.Map<List<GenreDto>>(genres);
        return genresDto;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GenreDto>> GetGenreByIdAsync([FromRoute] int id)
    {
        try
        {
            var genre = await _genreService.GetGenreByIdAsync(id);
            var genreDto = _mapper.Map<GenreDto>(genre);
            return genreDto;
        }
        catch (KeyNotFoundException keyNotFoundEx)
        {
            return NotFound(keyNotFoundEx.Message);
        }
    }
}