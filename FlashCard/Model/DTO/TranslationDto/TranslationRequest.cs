using System.ComponentModel.DataAnnotations;

namespace FlashCard.Model.DTO.TranslationDto;

public class TranslationRequest
{
	[Required]
	public Word SourceWord { get; set; }
	[Required]
	public Word TargetWord { get; set; }
}
