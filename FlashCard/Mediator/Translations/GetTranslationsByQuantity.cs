using FlashCard.Model;
using FlashCard.Shared.Services.Translations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlashCard.Mediator.Translations;

public class GetTranslationsByQuantity
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
			var randomCards = await GetTranslation.GetFalshCardsByParameters(_context,
																		TypeOfQueryTranslation.Quantity,
																		request.SourceLanguage, request.TargetLanguage,
																		quantity: request.Quantity);

			return randomCards;
		}
	}
}
