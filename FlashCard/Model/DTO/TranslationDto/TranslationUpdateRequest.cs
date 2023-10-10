using System.ComponentModel.DataAnnotations;

namespace FlashCard.Model.DTO.TranslationDto;

public class TranslationUpdateRequest
{
	[Required]
	public Guid SourceWordId { get; set; }
	[Required]
	public Guid TargetWordId { get; set; }

	public Translation ToTranslation()
	{
		return new Translation
		{
			SourceWordId = SourceWordId,
			TargetWordId = TargetWordId
		};
	}
}