using Microsoft.AspNetCore.Mvc;
using FlashCard.Mediator.Words;

namespace FlashCard.Controllers;

public class WordsController : BaseApiController
{
	[HttpGet]
	public async Task<ActionResult<List<Word>>> GetWords()
	{
		return await Mediator.Send(new ListWords.Query());
	}


	[HttpGet("{id}")]
	public async Task<ActionResult<Word>> GetWord(int id)
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
	public async Task<ActionResult> Edit(int id, Word word)
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
	public async Task<ActionResult> Delete(int id)
	{
		return Ok(await Mediator.Send(new DeleteWords.Command { Id = id }));
	}

}