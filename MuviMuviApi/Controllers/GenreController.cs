using System.Runtime.CompilerServices;
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
    public async Task<ActionResult<List<GenreDTO>>> GetAsync()
    {
        List<Genre> genres = await _genreService.GetAllGenresAsync();
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
    public async Task<ActionResult<GenreCreationDTO>> PostAsync([FromBody] GenreCreationDTO genreCreationDto)
    {
        Genre genre = _mapper.Map<Genre>(genreCreationDto);
        Genre genrePosted = await _genreService.PostGenreAsync(genre);
        GenreDTO genreResponse = _mapper.Map<GenreDTO>(genrePosted);
        return CreatedAtRoute("GetById", new { id = genreResponse.Id}, genreResponse);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<GenreDTO>> PutAsync([FromRoute] int id, [FromBody] GenreCreationDTO genreCreationDTO)
    {
        Genre genre = _mapper.Map<Genre>(genreCreationDTO);
        await _genreService.PutGenreAsync(id, genre);

        GenreDTO genreDto = _mapper.Map<GenreDTO>(genre);
        return Ok(genreDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await _genreService.DeleteGenreAsync(id);
        return NoContent();
    }
}