using Microsoft.AspNetCore.Mvc;
using FlashCard.Mediator.Words;
using FlashCard.Shared.Services.Translations;
using FlashCard.Model.DTO.WordDto;
using MediatR;

namespace FlashCard.Controllers;

/// <summary>
/// Word Controller
/// </summary>
public class WordsController : ControllerBase
{
	private readonly IMediator _mediator;

    public WordsController(IMediator mediator)
    {
        _mediator = mediator;
    }


    /// <summary>
    /// Get all words from DB
    /// </summary>
    /// <returns></returns>
    [HttpGet]
	public async Task<ActionResult<List<WordResponse>>> GetWords()
	{
		return await _mediator.Send(new GetWordsBy.Query
		{
			TypeOfQueryWord = TypeOfQueryWord.All
		});
	}


	/// <summary>
	/// Get all words by language
	/// </summary>
	/// <param name="targetLanguage">Get words by the language you want</param>
	/// <returns>List of words</returns>
	[HttpGet("byLanguage/{targetLanguage}")]
	public async Task<ActionResult<List<WordResponse>>> GetWordsByLanguage(string targetLanguage)
	{
		return await _mediator.Send(new GetWordsBy.Query
		{
			TypeOfQueryWord = TypeOfQueryWord.Language,
			TargetLanguage = targetLanguage
		});
	}


	/// <summary>
	/// Get a certain amount of words by a language
	/// </summary>
	/// <param name="targetLanguage">language, you want to get cards</param>
	/// <param name="quantity">the amount of the words</param>
	/// <returns>List of a certain amount of Words</returns>
	[HttpGet("byQuantity/{quantity}/{targetLanguage}")]
	public async Task<ActionResult<List<WordResponse>>> GetWordsByQuantity(string targetLanguage, int quantity)
	{
		//add verification for existing of the lang
		//upd, verification will not be implemented, will be a filter on client lvl
		return await _mediator.Send(new GetWordsBy.Query
		{
			TypeOfQueryWord = TypeOfQueryWord.Quantity,
			TargetLanguage = targetLanguage,
			Quantity = quantity
		});
	}

	/// <summary>
	/// Get a word by id
	/// </summary>
	/// <param name="id">id of the word</param>
	/// <returns></returns>
	[HttpGet("{id}")]
	public async Task<ActionResult<WordResponse>> GetWord(Guid id)
	{
		var word = new WordResponse();

		try
		{
			word = await _mediator.Send(new DetailsWordsById.Query { Id = id });
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}

		return Ok(word);
	}

	/// <summary>
	/// Create a word
	/// </summary>
	/// <param name="wordRequest">Dto for word request</param>
	/// <returns></returns>
	[HttpPost]
	public async Task<ActionResult> Create(WordRequest wordRequest)
	{
		try
		{
			await _mediator.Send(new CreateWords.Command
			{
				WordRequest = wordRequest,
				Mediator = _mediator
			});
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}

		return Ok();
	}

	/// <summary>
	/// Edit the word
	/// </summary>
	/// <param name="wordUpdateRequest">Dto for word updation</param>
	/// <returns></returns>
	[HttpPut]
	public async Task<ActionResult> Edit(WordUpdateRequest wordUpdateRequest)
	{
		return Ok(await _mediator.Send(new EditWords.Command { WordUpdateRequest = wordUpdateRequest, Mediator = _mediator }));
	}

	/// <summary>
	/// Delete a word by id
	/// </summary>
	/// <param name="id">Id of the word</param>
	/// <returns></returns>
	[HttpDelete("{id}")]
	public async Task<ActionResult> Delete(Guid id)
	{
		try
		{
			await _mediator.Send(new DeleteWords.Command { Id = id });
		}
		catch (ArgumentNullException ex)
		{
			return BadRequest(ex.Message);
		}

		return Ok();
	}
}