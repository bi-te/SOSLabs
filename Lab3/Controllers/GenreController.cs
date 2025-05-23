using GamesApi.DTOs;
using GamesApi.Entities;
using GamesApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.Controllers;

[ApiController]
[Route("api/genre")]
public class GenreController: ControllerBase
{
    private readonly GenreService _genreService;
    
    public GenreController(GenreService genreService)
    {
        _genreService = genreService;
    }
    
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GenreDto>>> GetGenres()
    {
        return Ok(await _genreService.GetGenresAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GenreDto>> GetGenre(int id)
    {
        GenreDto? genre = await _genreService.GetGenreAsync(id);
        return genre is not null ? Ok(genre) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<GenreDto>> CreateGenre(CreateGenreDto addDto)
    {
        GenreDto genre = await _genreService.AddGenreAsync(addDto);
        return CreatedAtAction(nameof(GetGenre), new RouteValueDictionary { { "id", genre.Id } }, genre);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateGenre(int id, CreateGenreDto updateDto)
    {
        await _genreService.EditGenre(id, updateDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteGenre(int id)
    {
        await _genreService.DeleteGenre(id);
        return NoContent();
    }
}