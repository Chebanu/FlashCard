using FlashCard.Shared.Enums;

namespace FlashCard.Model.DTO.WordDto;

public class WordUpdateRequest
{
	public Guid WordId { get; set; }
	public string WordText { get; set; }
	public string ThemeName { get; set; }
	public LanguageOfTheWord Language { get; set; }
	public LevelOfTheWord Level { get; set; }
	public string? ImageUrl { get; set; }
}
