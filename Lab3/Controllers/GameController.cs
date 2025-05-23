using GamesApi.DTOs;
using GamesApi.Entities;
using GamesApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.Controllers;

[ApiController]
[Route("api/game")]
public class GameController: ControllerBase
{
    private readonly GameService _gameService;
    
    public GameController(GameService gameService)
    {
        _gameService = gameService;
    }
    
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GameDto>>> GetGames()
    {
        return Ok(await _gameService.GetGamesAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GenreDto>> GetGame(int id)
    {
        GameDto? game = await _gameService.GetGameAsync(id);
        return game is not null ? Ok(game) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<GameDto>> CreateGame(CreateGameDto addDto)
    {
        try
        {
            GameDto game = await _gameService.AddGameAsync(addDto);
            return CreatedAtAction(nameof(GetGame), new RouteValueDictionary { { "id", game.Id } }, game);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateGame(int id, CreateGameDto updateDto)
    {
        await _gameService.EditGameAsync(id, updateDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteGame(int id)
    {
        await _gameService.DeleteGameAsync(id);
        return NoContent();
    }
}