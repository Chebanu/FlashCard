using FlashCard.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlashCard.Mediator.Words;

public class ListWords
{
	public class Query : IRequest<List<Word>> { }

	public class Handler : IRequestHandler<Query, List<Word>>
	{
		public readonly FlashCardDbContext _context;

		public Handler(FlashCardDbContext context)
		{
			_context = context;
		}

		public async Task<List<Word>> Handle(Query request, CancellationToken cancellationToken)
		{
			return await _context.Words.Include(l => l.Language)
										.Include(l=>l.Level)
									  .ToListAsync();
		}
	}
}
