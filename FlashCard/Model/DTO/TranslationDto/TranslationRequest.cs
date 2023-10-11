using System.ComponentModel.DataAnnotations;

namespace FlashCard.Model.DTO.TranslationDto;

public class TranslationRequest
{
	[Required]
	public Guid SourceLanguageId { get; set; }
	[Required]
	public Guid TargetLanguageId { get; set; }
}
