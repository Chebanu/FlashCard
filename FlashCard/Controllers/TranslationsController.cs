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

		var uniqueTranslations = await TranslationDistributor.Distribute(Mediator,
																			TypeOfQueryTranslation.All,
																			sourceLanguage,
																			targetLanguage);

		return Ok(uniqueTranslations);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<TranslationResponse>> GetTranslation(Guid id)
	{
		try
		{
			return await Mediator.Send(new DetailsTranslations.Query { Id = id });
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("quantity/{quantity}/{sourceLanguage}/{targetLanguage}")]
	public async Task<ActionResult<List<TranslationResponse>>> GetRandomQuantityOfCards(int quantity, string sourceLanguage, string targetLanguage)
	{
		//тоже самое что и с первым методом контроллера
		if (quantity <= 0 || sourceLanguage == targetLanguage)
			return BadRequest("Something went wrong");

		var uniqueTranslations = await TranslationDistributor.Distribute(Mediator,
																			TypeOfQueryTranslation.All,
																			sourceLanguage,
																			targetLanguage,
																			quantity: quantity);

		return Ok(uniqueTranslations.Take(quantity));
	}

	[HttpGet("level/{level}/{sourceLanguage}/{targetLanguage}")]
	public async Task<ActionResult<List<TranslationResponse>>> GetCardsByLevel(string level, string sourceLanguage, string targetLanguage)
	{
		//сейм щит
		if (sourceLanguage == targetLanguage)
			return BadRequest("Source and target languages must be different");

		var uniqueTranslations = await TranslationDistributor.Distribute(Mediator,
																			TypeOfQueryTranslation.Level,
																			sourceLanguage,
																			targetLanguage,
																			level: level);

		return uniqueTranslations;
	}

	[HttpPost]
	public async Task<ActionResult> Create(TranslationRequest translationRequest)
	{
		if (translationRequest.SourceWord.Language.LanguageName == translationRequest.TargetWord.Language.LanguageName)
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
			return BadRequest(ex.Message);
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
			return BadRequest(ex.Message);
		}

		return Ok();
	}
}