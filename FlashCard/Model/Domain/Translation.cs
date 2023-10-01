using System.ComponentModel.DataAnnotations;

public class Translation
{
    [Key]
    public int TranslationId { get; set; }
    public int SourceWordId { get; set; }
    public int TargetWordId { get; set; }

    public Word SourceWord { get; set; }
    public Word TargetWord { get; set; }
}