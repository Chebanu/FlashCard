using FlashCard.Model.Domain;
using System.ComponentModel.DataAnnotations;

public class Word
{
    [Key]
    public int WordId { get; set; }
    public string WordText { get; set; }
    public int LanguageId { get; set; }
    public int LevelId { get; set; }
    public string? ImageUrl { get; set; }

    public Language Language { get; set; }
    public Level Level { get; set; }
}