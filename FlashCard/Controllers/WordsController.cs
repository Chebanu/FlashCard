using Microsoft.AspNetCore.Mvc;
using FlashCard.Mediator.Words;
using FlashCard.Shared.Services.Translations;

namespace FlashCard.Controllers;

public class WordsController : BaseApiController
{
	[HttpGet]
	public async Task<ActionResult<List<Word>>> GetWords()
	{
		return await Mediator.Send(new ListWords.Query
		{
			TypeOfQueryWord = TypeOfQueryWord.All
		});
	}

	[HttpGet("byLanguage/{targetLanguage}")]
	public async Task<ActionResult<List<Word>>> GetWordsByLanguage(string targetLanguage)
	{
		return await Mediator.Send(new ListWords.Query
		{
			TypeOfQueryWord = TypeOfQueryWord.Language,
			TargetLanguage = targetLanguage
		});
	}

	[HttpGet("byQuantity/{quantity}/{targetLanguage}")]
	public async Task<ActionResult<List<Word>>> GetWordsByQuantity(string targetLanguage, int quantity)
	{
		return await Mediator.Send(new ListWords.Query
		{
			TypeOfQueryWord = TypeOfQueryWord.Quantity,
			TargetLanguage = targetLanguage,
			Quantity = quantity
		});
	}


	[HttpGet("{id}")]
	public async Task<ActionResult<Word>> GetWord(Guid id)
	{
		return await Mediator.Send(new DetailsWordsById.Query { Id = id });
	}

	[HttpPost]
	public async Task<ActionResult> Create(Word word)
	{
		var result = await CheckIfWordExists(word);

		if (result != null)
			return result;

		return Ok(await Mediator.Send(new CreateWords.Command { Word = word }));
	}

	[HttpPut("{id}")]
	public async Task<ActionResult> Edit(Guid id, Word word)
	{
		var result = await CheckIfWordExists(word);

		if (result != null)
			return result;

		word.WordId = id;
		return Ok(await Mediator.Send(new EditWords.Command { Word = word }));
	}

	private async Task<ActionResult> CheckIfWordExists(Word word)
	{
		var isWordExist = await Mediator.Send(new IsWordExist.Query
		{
			WordText = word.WordText,
			LanguageId = word.LanguageId
		});

		if (isWordExist)
			return BadRequest("The word is already exist in database");

		return null;
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult> Delete(Guid id)
	{
		return Ok(await Mediator.Send(new DeleteWords.Command { Id = id }));
	}
}