using FlashCard.Model;
using FlashCard.Shared.Services.Translations;
using MediatR;

namespace FlashCard.Mediator.Translations;

public class GetTranslationsByLevel
{
	public class Query : IRequest<List<Translation>>
	{
		public string Level { get; set; }
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
			var randomCards = await GetTranslation.GetFalshCardsByParameters(_context,
																		TypeOfQueryTranslation.Level,
																		request.SourceLanguage,
																		request.TargetLanguage,
																		level: request.Level);

			return randomCards;
		}
	}
}