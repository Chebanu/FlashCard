using Microsoft.AspNetCore.Mvc;
using FlashCard.Translations;
using Application.Activities;

namespace FlashCard.Controllers;

public class TranslationsController : BaseApiController
{
	[HttpGet]
	public async Task<ActionResult<List<Translation>>> GetTranslations()
	{
		return await Mediator.Send(new List.Query());
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Translation>> GetTranslation(int id)
	{
		return await Mediator.Send(new Details.Query { Id = id });
	}

	[HttpPost]
	public async Task<ActionResult> Create(Translation translation)
	{
		return Ok(await Mediator.Send(new Create.Command { Translation = translation }));
	}

	[HttpPut("{id}")]
	public async Task<ActionResult> Edit(int id, Translation translation)
	{
		translation.TranslationId = id;
		return Ok(await Mediator.Send(new Edit.Command { Translation = translation }));
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult> Delete(int id)
	{
		return Ok(await Mediator.Send(new Delete.Command { Id = id }));
	}
}