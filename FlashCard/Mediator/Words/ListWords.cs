using FlashCard.Model;
using FlashCard.Shared.Services.Translations;
using MediatR;

namespace FlashCard.Mediator.Words;

public class ListWords
{
	public class Query : IRequest<List<Word>>
	{
		public TypeOfQueryWord TypeOfQueryWord { get; set; }
		public string TargetLanguage { get; set; }
		public string Level { get; set; }
		public int Quantity { get; set; }
	}

	public class Handler : IRequestHandler<Query, List<Word>>
	{
		public readonly FlashCardDbContext _context;

		public Handler(FlashCardDbContext context)
		{
			_context = context;
		}

		public async Task<List<Word>> Handle(Query request, CancellationToken cancellationToken)
		{
			var words = new List<Word>();

			switch (request.TypeOfQueryWord)
			{
				case TypeOfQueryWord.All:
					words = await GetWord.GetFalshCardsByParameters(_context, request.TypeOfQueryWord);
					break;
				case TypeOfQueryWord.Level:
					words = await GetWord.GetFalshCardsByParameters(_context, request.TypeOfQueryWord, request.TargetLanguage, request.Level);
					break;
				case TypeOfQueryWord.Quantity:
					words = await GetWord.GetFalshCardsByParameters(_context, request.TypeOfQueryWord, request.TargetLanguage, quantity: request.Quantity);
					break;
				case TypeOfQueryWord.Language:
					words = await GetWord.GetFalshCardsByParameters(_context, request.TypeOfQueryWord, request.TargetLanguage);
					break;
				default:
					break;
			}

			return words;
		}
	}
}