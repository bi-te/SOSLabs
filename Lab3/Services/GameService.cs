using GamesApi.Data;
using GamesApi.DTOs;
using GamesApi.Entities;
using GamesApi.Mappings;
using Microsoft.EntityFrameworkCore;

namespace GamesApi.Services;

public class GameService(GamesDbContext dbContext)
{
    private GamesDbContext _dbContext = dbContext;

    public async Task<IEnumerable<GameDto>> GetGamesAsync()
    {
        return await _dbContext.Games.Include(game => game.Genre).Select(game => game.ToDto()).AsNoTracking().ToListAsync();
    }

    public async Task<GameDto?> GetGameAsync(int id)
    {
        return (await _dbContext.Games.FindAsync(id))?.ToDto();
    }

    public async Task<GameDto> AddGameAsync(CreateGameDto gameDto)
    {
        Genre? existingGenre = await _dbContext.Genres.FindAsync(gameDto.Genre);
        if (existingGenre is null)
        {
            throw new ArgumentException("GenreId is not valid");
        }
        
        Game game = new Game()
        {
            Name = gameDto.Name,
            GenreId = gameDto.Genre,
            Price = gameDto.Price,
            Score = gameDto.Score
        };

        var added = await _dbContext.Games.AddAsync(game);
        await _dbContext.SaveChangesAsync();

        return added.Entity.ToDto();
    }

    public async Task EditGameAsync(int id, CreateGameDto editGame)
    {
        Game? game = await _dbContext.Games.FindAsync(id);
        if (game is null)
        {
            await AddGameAsync(editGame);
            return;
        }

        Genre? searchGenre = await _dbContext.Genres.FindAsync(editGame.Genre);
        if (searchGenre is null)
        {
            throw new ArgumentException("GenreId is not valid");
        }

        game.Name = editGame.Name;
        game.GenreId = editGame.Genre;
        game.Genre = searchGenre;
        game.Price = editGame.Price;
        game.Score = editGame.Score;
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteGameAsync(int id)
    {
        await _dbContext.Games.Where(game => game.GameId == id)
            .ExecuteUpdateAsync(setters => setters.SetProperty(g => g.IsDeleted, true));
        //await _dbContext.Games.Where(game => game.GameId == id).ExecuteDeleteAsync();
    }
    
}