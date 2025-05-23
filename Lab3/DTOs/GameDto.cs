using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace GamesApi.DTOs;

public class GameDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public string? Genre { get; set; }
    public float Score { get; set; }
    public float Price { get; set; }
}