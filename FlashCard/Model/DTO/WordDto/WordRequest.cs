using System.ComponentModel.DataAnnotations;

namespace FlashCard.Model.DTO.WordDto;

public class WordRequest
{
	[Required]
	public string WordText { get; set; }
	[Required]
	public Guid LanguageId { get; set; }
	[Required]
	public Guid LevelId { get; set; }
}
