using FlashCard.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlashCard.Translations;

public class Details
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
			return await _context.Translations.Include(t => t.SourceWord).Include(t => t.TargetWord).Where(t => t.TranslationId == request.Id).FirstOrDefaultAsync(cancellationToken)!;
		}
	}
}