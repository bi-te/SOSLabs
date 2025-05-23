using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace GamesApi.DTOs;

public class CreateGameDto
{
    [Required]
    [StringLength(100)]
    public required string Name { get; set; }
    
    public int Genre { get; set; }
    
    [Range(0, 10)]
    public float Score { get; set; }
    
    [Range(0, 1000)]
    public float Price { get; set; }
    
    public DateOnly ReleaseDate { get; set; }
}