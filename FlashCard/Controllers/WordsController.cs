using Microsoft.AspNetCore.Mvc;
using FlashCard.Mediator.Words;
using FlashCard.Shared.Services.Translations;
using FlashCard.Model.DTO.WordDto;

namespace FlashCard.Controllers;

public class WordsController : BaseApiController
{
	[HttpGet]
	public async Task<ActionResult<List<WordResponse>>> GetWords()
	{
		return await Mediator.Send(new GetWordsBy.Query
		{
			TypeOfQueryWord = TypeOfQueryWord.All
		});
	}

	[HttpGet("byLanguage/{targetLanguage}")]
	public async Task<ActionResult<List<WordResponse>>> GetWordsByLanguage(string targetLanguage)
	{
		return await Mediator.Send(new GetWordsBy.Query
		{
			TypeOfQueryWord = TypeOfQueryWord.Language,
			TargetLanguage = targetLanguage
		});
	}

	[HttpGet("byQuantity/{quantity}/{targetLanguage}")]
	public async Task<ActionResult<List<WordResponse>>> GetWordsByQuantity(string targetLanguage, int quantity)
	{
		//добавить проверку на существование языка
		//upd, проверки не будет, будет фильтр на уровне клиента
		return await Mediator.Send(new GetWordsBy.Query
		{
			TypeOfQueryWord = TypeOfQueryWord.Quantity,
			TargetLanguage = targetLanguage,
			Quantity = quantity
		});
	}


	[HttpGet("{id}")]
	public async Task<ActionResult<WordResponse>> GetWord(Guid id)
	{
		var word = new WordResponse();

		try
		{
			word = await Mediator.Send(new DetailsWordsById.Query { Id = id });
		}
		catch(Exception ex)
		{
			return BadRequest($"{ex}");
		}

		return Ok(word);
	}

	[HttpPost]
	public async Task<ActionResult> Create(WordRequest wordRequest)
	{
		try
		{
			await Mediator.Send(new CreateWords.Command
			{
				WordRequest = wordRequest,
				Mediator = Mediator
			});
		}
		catch(Exception ex)
		{
			return BadRequest($"{ex}");
		}

		return Ok();
	}
	/*
		[HttpPut("{id}")]
		public async Task<ActionResult> Edit(Guid id, Word word)
		{
			word.WordId = id;
			return Ok(await Mediator.Send(new EditWords.Command { Word = word }));
		}*/


	[HttpDelete("{id}")]
	public async Task<ActionResult> Delete(Guid id)
	{
		try
		{
			await Mediator.Send(new DeleteWords.Command { Id = id });
		}
		catch(ArgumentNullException ex)
		{
			return BadRequest($"{ex}");
		}

		return Ok();
	}
}