using Microsoft.AspNetCore.Mvc;
using FlashCard.Mediator.Translations;
using Microsoft.AspNetCore.Authorization;
using FlashCard.Model.DTO;
using FlashCard.Mediator.Words;

namespace FlashCard.Controllers;

public class TranslationsController : BaseApiController
{
	[HttpGet]
	public async Task<ActionResult<List<Translation>>> GetTranslations(string sourceLanguage, string targetLanguage)
	{
		return await Mediator.Send(new ListTranslations.Query
		{
			SourceLanguage = sourceLanguage,
			TargetLanguage = targetLanguage
		});
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Translation>> GetTranslation(int id)
	{
		return await Mediator.Send(new DetailsTranslations.Query { Id = id });
	}

	[HttpGet("quantity/{quantity}/{sourceLanguage}/{targetLanguage}")]
	public async Task<ActionResult<List<Translation>>> GetRandomQuantityOfCards(int quantity, string sourceLanguage, string targetLanguage)
	{
		if (quantity <= 0 ||sourceLanguage == targetLanguage)
			return BadRequest("Something went wrong");

		var translations = await Mediator.Send(new GetTranslationsByQuantity.Query
		{
			SourceLanguage = sourceLanguage,
			TargetLanguage = targetLanguage
		});

		var reverseTranslations = await Mediator.Send(new GetTranslationsByQuantity.Query
		{
			SourceLanguage = targetLanguage,
			TargetLanguage = sourceLanguage
		});

		foreach (var translation in translations)
			(translation.SourceWord, translation.TargetWord) = (translation.TargetWord, translation.SourceWord);

		var concatTranslation = translations.Concat(reverseTranslations).Distinct().Take(quantity).ToList();

		return concatTranslation;
	}

	[HttpGet("level/{level}/{sourceLanguage}/{targetLanguage}")]
	public async Task<ActionResult<List<Translation>>> GetCardsByLevel(string level, string sourceLanguage, string targetLanguage)
	{
		if (sourceLanguage == targetLanguage)
			return BadRequest("Source language and target language must be different");

		var translations = await Mediator.Send(new GetTranslationsByLevel.Query
		{
			Level = level,
			SourceLanguage = sourceLanguage,
			TargetLanguage = targetLanguage
		});

		var reverseTranslations = await Mediator.Send(new GetTranslationsByLevel.Query
		{
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
	public async Task<ActionResult> Create(Translation translation)
	{
		if (translation.SourceWord == translation.TargetWord)
			return BadRequest("You can not add translation for the same language as source one");

		var result = await CheckIfTranslationExists(translation);

		if (result != null)
			return result;

		return Ok(await Mediator.Send(new CreateTranslations.Command { Translation = translation }));
	}

	[HttpPut("{id}")]
	public async Task<ActionResult> Edit(int id, Translation translation)
	{
		if (translation.SourceWord == translation.TargetWord)
			return BadRequest("You can not edit translation for the same language as source one");

		var result = await CheckIfTranslationExists(translation);

		if (result != null)
			return result;

		translation.TranslationId = id;
		return Ok(await Mediator.Send(new EditTranslations.Command { Translation = translation }));
	}

	private async Task<ActionResult> CheckIfTranslationExists(Translation translation)
	{
		var isTranslationExist = await Mediator.Send(new IsTranslationExist.Query
		{
			SourceId = translation.SourceWordId,
			TargetId = translation.TargetWordId
		});

		if(!isTranslationExist)
		{
			isTranslationExist = await Mediator.Send(new IsTranslationExist.Query
			{
				SourceId = translation.TargetWordId,
				TargetId = translation.SourceWordId
			});
		}

		if (isTranslationExist)
			return BadRequest("The transaltion is already exist in database");

		return null;
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult> Delete(int id)
	{
		return Ok(await Mediator.Send(new DeleteTranslations.Command { Id = id }));
	}
}