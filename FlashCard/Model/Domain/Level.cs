using System.ComponentModel.DataAnnotations;

public class Level
{
    [Key]
    public int LevelId { get; set; }
    public string LevelName { get; set; }
}