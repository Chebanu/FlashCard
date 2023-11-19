using Microsoft.AspNetCore.Mvc;
using FlashCard.Mediator.Translations;
using FlashCard.Shared.Services.Translations;
using FlashCard.Model.DTO.TranslationDto;
using MediatR;

namespace FlashCard.Controllers;

/// <summary>
/// Translation Controller manipulate with translation instances
/// </summary>
public class TranslationsController : ControllerBase
{
	private readonly IMediator _mediator;
	public TranslationsController(IMediator mediator)
	{
		_mediator = mediator;
	}
	/// <summary>
	/// Get list of translations sorting by source and target languages
	/// </summary>
	/// <param name="sourceLanguage">The language you currently learn</param>
	/// <param name="targetLanguage">The language which you already know</param>
	/// <returns></returns>
	[HttpGet]
	public async Task<ActionResult<List<TranslationResponse>>> GetTranslations(string sourceLanguage, string targetLanguage)
	{
		if (sourceLanguage == targetLanguage)
		{
			return BadRequest("Source and target languages must be different");
		}

		var uniqueTranslations = await TranslationDistributor.Distribute(_mediator,
																			TypeOfQueryTranslation.All,
																			sourceLanguage,
																			targetLanguage);

		return Ok(uniqueTranslations);
	}

	/// <summary>
	/// Get 1 translation by Guid
	/// </summary>
	/// <param name="id">Translation id property</param>
	/// <returns></returns>
	[HttpGet("{id}")]
	public async Task<ActionResult<TranslationResponse>> GetTranslation(Guid id)
	{
		try
		{
			return await _mediator.Send(new DetailsTranslation.Query { Id = id });
		}
		catch
		{
			return BadRequest("Something went wrong");
		}
	}

	/// <summary>
	/// Get certain amount of translations by source and target language
	/// </summary>
	/// <param name="quantity">Define the quantity of cards you'll from db</param>
	/// <param name="sourceLanguage">The language you currently learn</param>
	/// <param name="targetLanguage">The language which you already know</param>
	/// <returns>Certain amount of flash cards</returns>
	[HttpGet("quantity/{quantity}/{sourceLanguage}/{targetLanguage}")]
	public async Task<ActionResult<List<TranslationResponse>>> GetRandomQuantityOfCards(int quantity, string sourceLanguage, string targetLanguage)
	{
		if (quantity <= 0 || sourceLanguage == targetLanguage)
		{
			return BadRequest("Quantity must be more than 0 and languages must be different");
		}

		var uniqueTranslations = await TranslationDistributor.Distribute(_mediator,
																			TypeOfQueryTranslation.Quantity,
																			sourceLanguage,
																			targetLanguage,
																			quantity: quantity);

		return Ok(uniqueTranslations.Take(quantity));
	}

	/// <summary>
	/// Get translations by their level. Sorted by source and target languages
	/// </summary>
	/// <param name="level">Define the level of source word(A1,A2,B1 etc.)</param>
	/// <param name="sourceLanguage">The language you currently learn</param>
	/// <param name="targetLanguage">The language which you already know</param>
	/// <returns></returns>
	[HttpGet("level/{level}/{sourceLanguage}/{targetLanguage}")]
	public async Task<ActionResult<List<TranslationResponse>>> GetCardsByLevel(string level, string sourceLanguage, string targetLanguage)
	{
		if (sourceLanguage == targetLanguage)
		{
			return BadRequest("Source and target languages must be different");
		}

		var uniqueTranslations = await TranslationDistributor.Distribute(_mediator,
																			TypeOfQueryTranslation.Level,
																			sourceLanguage,
																			targetLanguage,
																			level: level);

		return uniqueTranslations;
	}

	/// <summary>
	/// Create a translation
	/// </summary>
	/// <param name="translationRequest">Dto for translation request</param>
	/// <returns></returns>
	[HttpPost]
	public async Task<ActionResult> Create(TranslationRequest translationRequest)
	{
		if (translationRequest.SourceLanguage == translationRequest.TargetLanguage)
		{
			return BadRequest("You can not add translation for the same language as source one");
		}

		Guid createdId;

		try
		{
			createdId = await _mediator.Send(new CreateTranslations.Command
			{
				TranslationRequest = translationRequest,
				Mediator = _mediator
			});
		}
		catch (ArgumentNullException ex)
		{
			return BadRequest($"Argument exception, {ex}");
		}
		catch
		{
			return BadRequest("Something went wrong");
		}

		return CreatedAtAction("The translation is succesfully saved. Your id", createdId);
	}


	/// <summary>
	/// Edit translation
	/// </summary>
	/// <param name="translationUpdateRequest"></param>
	/// <returns></returns>
	[HttpPut]
	public async Task<ActionResult<TranslationResponse>> Edit(TranslationUpdateRequest translationUpdateRequest)
	{
		if (translationUpdateRequest.SourceWord == translationUpdateRequest.TargetWord &&
			translationUpdateRequest.SourceLanguage == translationUpdateRequest.TargetLanguage)
			return BadRequest("You can not edit translation for the same language as source one");

		var newTranslation = new TranslationResponse();

		try
		{
			newTranslation = await _mediator.Send(new EditTranslations.Command
			{
				TranslationUpdateRequest = translationUpdateRequest,
				Mediator = _mediator
			});
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}

		return newTranslation;
	}


	/// <summary>
	/// Delete translation
	/// </summary>
	/// <param name="id">Delete by id</param>
	/// <returns></returns>
	[HttpDelete("{id}")]
	public async Task<ActionResult> Delete(Guid id)
	{
		try
		{
			await _mediator.Send(new DeleteTranslations.Command { Id = id });
		}
		catch
		{
			return BadRequest("The translation is not found");
		}

		return NoContent();
	}
}