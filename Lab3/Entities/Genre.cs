namespace GamesApi.Entities;

public class Genre
{
    public int GenreId { get; set; }
    public required string Name { get; set; }
    
    public bool IsDeleted { get; set; }
}