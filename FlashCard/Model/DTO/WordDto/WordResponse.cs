namespace FlashCard.Model.DTO.WordDto;

public class WordResponse
{
	public string WordText { get; set; }
	public string? ImageUrl { get; set; }
	public string Language { get; set; }
	public string Level { get; set; }
}
