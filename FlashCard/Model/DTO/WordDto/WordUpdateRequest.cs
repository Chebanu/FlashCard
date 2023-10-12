namespace FlashCard.Model.DTO.WordDto;

public class WordUpdateRequest
{
	public string WordText { get; set; }
	public Guid LanguageId { get; set; }
	public Guid LevelId { get; set; }
}
