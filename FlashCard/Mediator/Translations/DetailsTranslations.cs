using FlashCard.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlashCard.Mediator.Translations;

public class DetailsTranslations
{
	public class Query : IRequest<Translation>
	{
		public int Id { get; set; }
	}
	public class Handler : IRequestHandler<Query, Translation>
	{
		private readonly FlashCardDbContext _context;

		public Handler(FlashCardDbContext context)
		{
			_context = context;
		}

		public async Task<Translation> Handle(Query request, CancellationToken cancellationToken)
		{
			var translation = await _context.Translations
				.Include(t => t.SourceWord)
				.Include(t => t.TargetWord)
				.Include(s=>s.SourceWord.Language)
				.Include(s => s.TargetWord.Language)
				.Include(s => s.SourceWord.Level)
				.Include(s => s.TargetWord.Level)
				.Where(t => t.TranslationId == request.Id)
				.FirstOrDefaultAsync(cancellationToken);

			return translation;
		}

	}
}