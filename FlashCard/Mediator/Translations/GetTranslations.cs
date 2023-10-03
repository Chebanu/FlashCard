using FlashCard.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlashCard.Mediator.Translations;

public class GetTranslations
{
	public class Query : IRequest<List<Translation>>
	{
		public int Quantity { get; set; }
		public string SourceLanguage { get; set; }
		public string TargetLanguage { get; set; }
	}

	public class Handler : IRequestHandler<Query, List<Translation>>
	{
		private readonly FlashCardDbContext _context;

		public Handler(FlashCardDbContext context)
		{
			_context = context;
		}

		public async Task<List<Translation>> Handle(Query request, CancellationToken cancellationToken)
		{
			var randomCards = await (_context.Translations
				.Include(s => s.SourceWord.Language)
				 .Include(s => s.TargetWord.Language)
				 .Include(s => s.SourceWord.Level)
				 .Include(s => s.TargetWord.Level)
				 .Select(x => new Translation
				 {
					 TranslationId = x.TranslationId,
					 SourceWord = x.SourceWord,
					 TargetWord = x.TargetWord,
				 })
				 .Where(x => x.SourceWord.Language.LanguageName == request.SourceLanguage)
				 .Where(x => x.TargetWord.Language.LanguageName == request.TargetLanguage)
				 .Take(request.Quantity)
				 ).ToListAsync();

			return randomCards;
		}
	}
}
