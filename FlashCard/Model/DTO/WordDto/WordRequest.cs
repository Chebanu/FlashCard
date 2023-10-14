using FlashCard.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace FlashCard.Model.DTO.WordDto;

public class WordRequest
{
	[Required]
	public string WordText { get; set; }
	[Required]
	public LanguageOfTheWord Language { get; set; }
	[Required]
	public LevelOfTheWord Level { get; set; }
	[Required]
	public string ThemeName { get; set; }
	public string? ImageUrl { get; set; }
}
