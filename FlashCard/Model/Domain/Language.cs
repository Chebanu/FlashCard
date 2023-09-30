using System.ComponentModel.DataAnnotations;

namespace FlashCard.Model.Domain;

public class Language
{
    [Key]
    public int LanguageId { get; set; }
    public string LanguageName { get; set; }
}

public class Level
{
    [Key]
    public int LevelId { get; set; }
    public string LevelName { get; set; }
}

public class Word
{
    [Key]
    public int WordId { get; set; }
    public string WordText { get; set; }
    public int LanguageId { get; set; } // Ссылка на Language
    public int LevelId { get; set; }   // Ссылка на Level
    public string? ImageUrl { get; set; }

    public Language Language { get; set; } // Навигационное свойство к Language
    public Level Level { get; set; }       // Навигационное свойство к Level
}

public class Translation
{
    [Key]
    public int TranslationId { get; set; }
    public int SourceWordId { get; set; }
    public int TargetWordId { get; set; }

    public Word SourceWord { get; set; } // Навигационное свойство к SourceWord
    public Word TargetWord { get; set; } // Навигационное свойство к TargetWord
}

