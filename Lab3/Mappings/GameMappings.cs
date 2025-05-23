using GamesApi.DTOs;
using GamesApi.Entities;

namespace GamesApi.Mappings;

public static class GameMappings
{
    public static GameDto ToDto(this Game game)
    {
        return new GameDto()
        {
            Id = game.GameId,
            Name = game.Name,
            Genre = game.Genre?.Name,
            Price = game.Price,
            Score = game.Score
        };
    }
}