using GamesApi.DTOs;
using GamesApi.Entities;

namespace GamesApi.Mappings;

public static class GenreMappings
{
    public static GenreDto ToDto(this Genre genre)
    {
        return new GenreDto()
        {
            Id = genre.GenreId,
            Name = genre.Name
        };
    }
}