using FlashCard.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlashCard.Mediator.Words;

public class DetailsWordsById
{
	public class Query : IRequest<Word>
	{
		public int Id { get; set; }
	}
	public class Handler : IRequestHandler<Query, Word>
	{
		private readonly FlashCardDbContext _context;

		public Handler(FlashCardDbContext context)
		{
			_context = context;
		}

		public async Task<Word> Handle(Query request, CancellationToken cancellationToken)
		{
			return await _context.Words.Include(w => w.Language)
										.Include(w => w.Level)
									.FirstOrDefaultAsync(cancellationToken);
		}
	}
}