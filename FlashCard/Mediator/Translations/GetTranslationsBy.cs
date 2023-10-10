using FlashCard.Model;
using FlashCard.Shared.Services.Translations;
using MediatR;

namespace FlashCard.Mediator.Translations;

public class GetTranslationsBy
{
	public class Query : IRequest<List<Translation>>
	{
		public TypeOfQueryTranslation TypeOfQueryTranslation { get; set; }
		public string Level { get; set; }
		public string SourceLanguage { get; set; }
		public string TargetLanguage { get; set; }
		public int Quantity { get; set; }
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
			var translations = new List<Translation>();

			switch (request.TypeOfQueryTranslation)
			{
				case TypeOfQueryTranslation.All:
					translations = await GetTranslation.GetFalshCardsByParameters(_context,
															request.TypeOfQueryTranslation,
															request.SourceLanguage,
															request.TargetLanguage);
					break;
				case TypeOfQueryTranslation.Level:
					translations = await GetTranslation.GetFalshCardsByParameters(_context, request.TypeOfQueryTranslation, request.SourceLanguage, request.TargetLanguage, request.Level);
					break;
				case TypeOfQueryTranslation.Quantity:
					translations = await GetTranslation.GetFalshCardsByParameters(_context,
															request.TypeOfQueryTranslation,
															request.SourceLanguage,
															request.TargetLanguage,
															quantity: request.Quantity);
					break;
				default:
					break;
			}

			return translations;

			/*return words;

			var rando1mCards = await GetTranslation.GetFalshCardsByParameters(_context, request.SourceLanguage, request.TargetLanguage);

			var randomCards = await GetTranslation.GetFalshCardsByParameters(_context,
																		TypeOfQueryTranslation.Level,
																		request.SourceLanguage,
																		request.TargetLanguage,
																		level: request.Level);

			return randomCards;*/
		}
	}
}