using Microsoft.AspNetCore.Mvc;
using FlashCard.Mediator.Translations;
using FlashCard.Shared.Services.Translations;
using FlashCard.Model.DTO.TranslationDto;

namespace FlashCard.Controllers;

public class TranslationsController : BaseApiController
{
	[HttpGet]
	public async Task<ActionResult<List<TranslationResponse>>> GetTranslations(string sourceLanguage, string targetLanguage)
	{
		if (sourceLanguage == targetLanguage)
			return BadRequest("Source and target languages must be different");

		var translations = await Mediator.Send(new GetTranslationsBy.Query
		{
			TypeOfQueryTranslation = TypeOfQueryTranslation.All,
			SourceLanguage = sourceLanguage,
			TargetLanguage = targetLanguage
		});

		var reverseTranslations = await Mediator.Send(new GetTranslationsBy.Query
		{
			TypeOfQueryTranslation = TypeOfQueryTranslation.All,
			SourceLanguage = targetLanguage,
			TargetLanguage = sourceLanguage
		});

		var allTranslations = translations.Concat(reverseTranslations).ToList();

		var uniqueTranslations = new List<TranslationResponse>();
		foreach (var translation in allTranslations)
		{
			bool isDuplicate = uniqueTranslations.Any(t =>
				(t.SourceWord == translation.SourceWord && t.TargetWord == translation.TargetWord) ||
				(t.SourceWord == translation.TargetWord && t.TargetWord == translation.SourceWord));

			if (!isDuplicate)
			{
				uniqueTranslations.Add(translation);
			}
		}

		return Ok(uniqueTranslations);
	}



	[HttpGet("{id}")]
	public async Task<ActionResult<TranslationResponse>> GetTranslation(Guid id)
	{
		// работает, необходимо добавить проверку на существования перевода
		return await Mediator.Send(new DetailsTranslations.Query { Id = id });
	}

	[HttpGet("quantity/{quantity}/{sourceLanguage}/{targetLanguage}")]
	public async Task<ActionResult<List<TranslationResponse>>> GetRandomQuantityOfCards(int quantity, string sourceLanguage, string targetLanguage)
	{
		//тоже самое что и с первым методом контроллера
		if (quantity <= 0 || sourceLanguage == targetLanguage)
			return BadRequest("Something went wrong");

		var translations = await Mediator.Send(new GetTranslationsBy.Query
		{
			TypeOfQueryTranslation = TypeOfQueryTranslation.Quantity,
			Quantity = quantity,
			SourceLanguage = sourceLanguage,
			TargetLanguage = targetLanguage
		});

		var reverseTranslations = await Mediator.Send(new GetTranslationsBy.Query
		{
			TypeOfQueryTranslation = TypeOfQueryTranslation.Quantity,
			Quantity = quantity,
			SourceLanguage = targetLanguage,
			TargetLanguage = sourceLanguage
		});

		foreach (var translation in translations)
			(translation.SourceWord, translation.TargetWord) = (translation.TargetWord, translation.SourceWord);

		var concatTranslation = translations.Concat(reverseTranslations).Distinct().Take(quantity).ToList();

		return concatTranslation;
	}

	[HttpGet("level/{level}/{sourceLanguage}/{targetLanguage}")]
	public async Task<ActionResult<List<TranslationResponse>>> GetCardsByLevel(string level, string sourceLanguage, string targetLanguage)
	{
		//сейм щит
		if (sourceLanguage == targetLanguage)
			return BadRequest("Source and target languages must be different");

		var translations = await Mediator.Send(new GetTranslationsBy.Query
		{
			TypeOfQueryTranslation = TypeOfQueryTranslation.Level,
			Level = level,
			SourceLanguage = sourceLanguage,
			TargetLanguage = targetLanguage
		});

		var reverseTranslations = await Mediator.Send(new GetTranslationsBy.Query
		{
			TypeOfQueryTranslation = TypeOfQueryTranslation.Level,
			Level = level,
			SourceLanguage = targetLanguage,
			TargetLanguage = sourceLanguage
		});

		foreach (var translation in translations)
			(translation.SourceWord, translation.TargetWord) = (translation.TargetWord, translation.SourceWord);

		var concatTranslation = translations.Concat(reverseTranslations).Distinct().ToList();

		return concatTranslation;
	}

	[HttpPost]
	public async Task<ActionResult> Create(TranslationRequest translationRequest)
	{
		if (translationRequest.SourceLanguageId == translationRequest.TargetLanguageId)
			return BadRequest("You can not add translation for the same language as source one");

		try
		{
			await Mediator.Send(new CreateTranslations.Command
			{
				TranslationRequest = translationRequest,
				Mediator = Mediator
			});
		}
		catch (ArgumentNullException ex)
		{
			return BadRequest($"Argument exception, {ex}");
		}
		catch (Exception ex)
		{
			return BadRequest($"{ex}");
		}

		return Ok();
	}

	/*[HttpPut("{id}")]
	public async Task<ActionResult> Edit(Guid id, TranslationUpdateRequest translationUpdateRequest)
	{
		if (translationUpdateRequest.SourceWordId == translationUpdateRequest.TargetWordId)
			return BadRequest("You can not edit translation for the same language as source one");

		return Ok(await Mediator.Send(new EditTranslations.Command
		{
			TranslationId = id,
			TranslationUpdateRequest = translationUpdateRequest,
			Mediator = Mediator
		}));
	}*/



	[HttpDelete("{id}")]
	public async Task<ActionResult> Delete(Guid id)
	{
		try
		{
			await Mediator.Send(new DeleteTranslations.Command { Id = id });
		}
		catch (Exception ex)
		{
			return BadRequest($"{ex}");
		}

		return Ok();
	}
}