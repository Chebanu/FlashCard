using FlashCard.Model.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Theme
{
	[Key]
	public Guid ThemeId { get; set; }

	public string ThemeName { get; set; }

	[ForeignKey("Language")]
	public Guid LanguageId { get; set; }

	public Language Language { get; set; }
}
