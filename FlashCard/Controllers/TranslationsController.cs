using Microsoft.AspNetCore.Mvc;
using FlashCard.Mediator.Translations;

namespace FlashCard.Controllers;

public class TranslationsController : BaseApiController
{
	[HttpGet]
	public async Task<ActionResult<List<Translation>>> GetTranslations()
	{
		return await Mediator.Send(new ListTranslations.Query());
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Translation>> GetTranslation(int id)
	{
		return await Mediator.Send(new DetailsTranslations.Query { Id = id });
	}

	[HttpPost]
	public async Task<ActionResult> Create(Translation translation)
	{
		return Ok(await Mediator.Send(new CreateTranslations.Command { Translation = translation }));
	}

	[HttpPut("{id}")]
	public async Task<ActionResult> Edit(int id, Translation translation)
	{
		translation.TranslationId = id;
		return Ok(await Mediator.Send(new EditTranslations.Command { Translation = translation }));
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult> Delete(int id)
	{
		return Ok(await Mediator.Send(new DeleteTranslations.Command { Id = id }));
	}
}