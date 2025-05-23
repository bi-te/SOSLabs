using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace GamesApi.DTOs;

public class CreateGenreDto
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public required string Name { get; set; }
}