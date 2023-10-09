using FlashCard.Model;
using Microsoft.EntityFrameworkCore;

namespace FlashCard.Shared.Services.Translations;

public enum TypeOfQueryTranslation
{
	Level,
	Quantity,
}

public class GetTranslation
{
	public static async Task<List<Translation>> GetFlashCards(FlashCardDbContext context,
													string sourceLang,
													string targetLang,
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
						})
						.Where(s => s.SourceWord.Language.LanguageName == sourceLang)
						.Where(r => r.TargetWord.Language.LanguageName == targetLang);

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
			case TypeOfQueryTranslation.Level:
				if (!string.IsNullOrWhiteSpace(level))
					translations = await GetFlashCards(context, sourceLang, targetLang, query => query
													.Where(t => t.SourceWord.Level.LevelName == level));
				break;/*
			case TypeOfQueryTranslation.Quantity:
				if (quantity > 0)
					translations = await GetFlashCards(context, sourceLang, targetLang, query => query.Take(quantity));
				break;*/
			default:
				throw new ArgumentException("Unsupported query type.", nameof(typeOfQueryTranslation));
		}

		return translations;
	}
}
