using Microsoft.AspNetCore.Mvc;
using FlashCard.Mediator.Translations;

namespace FlashCard.Controllers;

public class TranslationsController : BaseApiController
{
	[HttpGet]
	public async Task<ActionResult<List<Translation>>> GetTranslations()
	{
		return await Mediator.Send(new ListTranslations.Query());
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Translation>> GetTranslation(int id)
	{
		return await Mediator.Send(new DetailsTranslations.Query { Id = id });
	}

	[HttpGet("quantity/{quantity}/{sourceLanguage}/{targetLanguage}")]
	public async Task<ActionResult<List<Translation>>> GetRandomQuantityOfCards(int quantity, string sourceLanguage, string targetLanguage)
	{
		var translations = await Mediator.Send(new GetTranslationsByQuantity.Query
		{
			Quantity = quantity,
			SourceLanguage = sourceLanguage,
			TargetLanguage = targetLanguage
		});

		if(translations.Count == 0 && sourceLanguage != targetLanguage)
		{
			translations = await Mediator.Send(new GetTranslationsByQuantity.Query
			{	
				Quantity = quantity,
				SourceLanguage = targetLanguage,
				TargetLanguage = sourceLanguage
			});

			foreach (var translation in translations)
			{
				(translation.SourceWord, translation.TargetWord) = (translation.TargetWord, translation.SourceWord);
			}
		}

		return translations;
	}

	[HttpGet("level/{level}/{sourceLanguage}/{targetLanguage}")]
	public async Task<ActionResult<List<Translation>>> GetCardsByLevel(string level, string sourceLanguage, string targetLanguage)
	{
		var translations = await Mediator.Send(new GetTranslationsByLevel.Query
		{
			Level = level,
			SourceLanguage = sourceLanguage,
			TargetLanguage = targetLanguage
		});

		if (translations.Count == 0 && sourceLanguage != targetLanguage)
		{
			translations = await Mediator.Send(new GetTranslationsByLevel.Query
			{
				Level = level,
				SourceLanguage = targetLanguage,
				TargetLanguage = sourceLanguage
			});

			foreach (var translation in translations)
			{
				(translation.SourceWord, translation.TargetWord) = (translation.TargetWord, translation.SourceWord);
			}
		}

		return translations;
	}

	[HttpPost]
	public async Task<ActionResult> Create(Translation translation)
	{
		return Ok(await Mediator.Send(new CreateTranslations.Command { Translation = translation }));
	}

	[HttpPut("{id}")]
	public async Task<ActionResult> Edit(int id, Translation translation)
	{
		translation.TranslationId = id;
		return Ok(await Mediator.Send(new EditTranslations.Command { Translation = translation }));
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult> Delete(int id)
	{
		return Ok(await Mediator.Send(new DeleteTranslations.Command { Id = id }));
	}
}