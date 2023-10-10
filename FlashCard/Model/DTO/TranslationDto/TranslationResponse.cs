namespace FlashCard.Model.DTO.TranslationDto;

public class TranslationResponse
{
	public Guid TranslationId { get; set; }
	public Guid SourceWordId { get; set; }
	public Guid TargetWordId { get; set; }
	public string SourceWord { get; set; }
	public string TargetWord { get; set; }
	public string SourceLanguageName { get; set; }
	public string TargetLanguageName { get; set; }
	public string SourceWordLevelName { get; set; }
	public string? Image { get; set; }

	public TranslationUpdateRequest ToTranslationUpdateRequest()
	{
		return new TranslationUpdateRequest
		{
			SourceWordId = SourceWordId,
			TargetWordId = TargetWordId
		};
	}
}
public static class TranslationExtensions
{
	public static TranslationResponse ToTranslationResponse(this Translation translation)
	{
		return new TranslationResponse
		{
			TranslationId = translation.TranslationId,
			SourceWordId = translation.SourceWordId,
			TargetWordId = translation.TargetWordId,
			SourceWord = translation.SourceWord.WordText,
			TargetWord = translation.TargetWord.WordText,
			SourceLanguageName = translation.SourceWord.Language.LanguageName,
			TargetLanguageName = translation.TargetWord.Language.LanguageName,
			SourceWordLevelName = translation.SourceWord.Level.LevelName,
			Image = translation.SourceWord.ImageUrl != null ? translation.SourceWord.ImageUrl : null
		};
	}
}
