namespace GamesApi.Entities;

public class Game
{
    public int GameId { get; set; }
    public required string Name { get; set; }
    
    public int GenreId { get; set; }
    
    public Genre? Genre { get; set; }
    public float Score { get; set; }
    public float Price { get; set; }
    
    public bool IsDeleted { get; set; }
}