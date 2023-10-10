using FlashCard.Model.Domain;
using System.ComponentModel.DataAnnotations;

public class Word
{
	[Key]
	public Guid WordId { get; set; }
	public string WordText { get; set; }
	public Guid LanguageId { get; set; }
	public Guid LevelId { get; set; }
	public string? ImageUrl { get; set; }

	public Language Language { get; set; }
	public Level Level { get; set; }
}