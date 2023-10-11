namespace FlashCard.Model.DTO.TranslationDto;

public class TranslationResponse
{
	public string SourceWord { get; set; }
	public string TargetWord { get; set; }
	public string SourceLanguageName { get; set; }
	public string TargetLanguageName { get; set; }
	public string SourceWordLevelName { get; set; }
	public string? Image { get; set; }
}