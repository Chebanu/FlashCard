using System.ComponentModel.DataAnnotations.Schema;

public class Translation
{
	public int TranslationId { get; set; }
	public int SourceWordId { get; set; }
	public int TargetWordId { get; set; }

	[ForeignKey("SourceWordId")]
	public Word SourceWord { get; set; }

	[ForeignKey("TargetWordId")]
	public Word TargetWord { get; set; }
}
