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
	public async Task<ActionResult<Word>> GetTranslation(int id)
	{
		return await Mediator.Send(new DetailsWords.Query { Id = id });
	}

	[HttpPost]
	public async Task<ActionResult> Create(Word word)
	{
		return Ok(await Mediator.Send(new CreateWords.Command { Word = word }));
	}

	[HttpPut("{id}")]
	public async Task<ActionResult> Edit(int id, Word word)
	{
		word.WordId = id;
		return Ok(await Mediator.Send(new EditWords.Command { Word = word }));
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult> Delete(int id)
	{
		return Ok(await Mediator.Send(new DeleteWords.Command { Id = id }));
	}

}