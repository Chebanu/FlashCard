using System.ComponentModel.DataAnnotations;

namespace FlashCard.Model.Domain;

public class Language
{
    [Key]
    public int LanguageId { get; set; }
    public string LanguageName { get; set; }
}

