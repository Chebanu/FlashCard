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
			return BadRequest("Source language and target language must be different");

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

		foreach (var translation in translations)
		{
			(translation.SourceWord, translation.TargetWord) =
				(translation.TargetWord, translation.SourceWord);
			(translation.SourceLanguageName, translation.TargetLanguageName) =
				(translation.TargetLanguageName, translation.SourceLanguageName);
		}

		//перевернутый ответ, но правильный по структуре(немного доделать)

		var concatTranslation = translations.Concat(reverseTranslations).Distinct().ToList();

		return Ok(concatTranslation);
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
			return BadRequest("Source language and target language must be different");

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
		//работает отлично
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
		catch (Exception ex)
		{
			return BadRequest(ex);
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
		//с существующим нормально удаляет, с несущ 500 ошибка
		try
		{
			await Mediator.Send(new DeleteTranslations.Command { Id = id });
		}
		catch (Exception ex)
		{
			return BadRequest(ex);
		}

		return Ok();
	}
}