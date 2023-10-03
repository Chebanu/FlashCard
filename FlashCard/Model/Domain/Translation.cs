using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

[Index(nameof(SourceWordId), nameof(TargetWordId), Name = "IX_SourceTarget", IsUnique = true)]
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
