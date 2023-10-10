using FlashCard.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlashCard.Mediator.Translations;

public class DeleteTranslations
{
	public class Command : IRequest
	{
		public Guid Id { get; set; }
	}

	public class Handler : IRequestHandler<Command>
	{
		private readonly FlashCardDbContext _context;
		public Handler(FlashCardDbContext context)
		{
			_context = context;
		}
		public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
		{
			var translation = await _context.Translations.Where(x => x.TranslationId == request.Id)
														.FirstOrDefaultAsync(cancellationToken);

			_context.Remove(translation);

			await _context.SaveChangesAsync();

			return Unit.Value;
		}
	}
}
