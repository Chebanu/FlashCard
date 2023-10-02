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
			return await _context.Translations.Include(t => t.SourceWord)
												.Include(l => l.SourceWord.Level)
												.Include(l => l.SourceWord.Language)
												.Include(t => t.TargetWord)
												.Include(l => l.TargetWord.Level)
												.Include(l => l.TargetWord.Language)
												.Where(t => t.TranslationId == request.Id)
											.FirstOrDefaultAsync(cancellationToken);
		}
	}
}