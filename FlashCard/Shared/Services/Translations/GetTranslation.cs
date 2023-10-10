using FlashCard.Model;
using Microsoft.EntityFrameworkCore;

namespace FlashCard.Shared.Services.Translations;

public enum TypeOfQueryTranslation
{
	All,
	Level,
	Quantity,
}

public class GetTranslation
{
	private static async Task<List<Translation>> GetFlashCards(FlashCardDbContext context,
													Func<IQueryable<Translation>, IQueryable<Translation>> filter = null)
	{
		var query = context.Translations
							.Include(s => s.SourceWord.Language)
							.Include(s => s.TargetWord.Language)
							.Include(s => s.SourceWord.Level)
							.Include(s => s.TargetWord.Level)
						.Select(x => new Translation
						{
							TranslationId = x.TranslationId,
							SourceWord = x.SourceWord,
							TargetWord = x.TargetWord,
						});

		if (filter != null)
		{
			query = filter(query);
		}

		var randomCards = await query.ToListAsync();
		return randomCards;
	}


	public static async Task<List<Translation>> GetFalshCardsByParameters(FlashCardDbContext context,
															TypeOfQueryTranslation typeOfQueryTranslation,
															string sourceLang,
															string targetLang,
															string level = null,
															int quantity = 0)
	{
		List<Translation> translations = new List<Translation>();

		switch (typeOfQueryTranslation)
		{
			case TypeOfQueryTranslation.All:
				translations = await GetFlashCards(context);
				break;
			case TypeOfQueryTranslation.Level:
				if (!string.IsNullOrWhiteSpace(level) &&
					!string.IsNullOrWhiteSpace(targetLang) &&
					!string.IsNullOrWhiteSpace(sourceLang))
				{
					translations = await GetFlashCards(context,
									query => query.Where(t => t.SourceWord.Level.LevelName == level)
										.Where(s => s.SourceWord.Language.LanguageName == sourceLang)
										.Where(r => r.TargetWord.Language.LanguageName == targetLang));
				}
				break;
			case TypeOfQueryTranslation.Quantity:
				if (quantity > 0 &&
					!string.IsNullOrWhiteSpace(targetLang) &&
					!string.IsNullOrWhiteSpace(sourceLang))
				{
					translations = await GetFlashCards(context,
									query => query.Where(s => s.SourceWord.Language.LanguageName == sourceLang)
										.Where(r => r.TargetWord.Language.LanguageName == targetLang)
										.OrderBy(x => Guid.NewGuid())
										.Take(quantity));
				}
				break;
			default:
				throw new ArgumentException("Unsupported query type.", nameof(typeOfQueryTranslation));
		}

		return translations;
	}
}