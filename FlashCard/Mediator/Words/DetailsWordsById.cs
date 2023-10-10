using FlashCard.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlashCard.Mediator.Words;

public class DetailsWordsById
{
	public class Query : IRequest<Word>
	{
		public Guid Id { get; set; }
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
			return await _context.Words.Where(x=>x.WordId == request.Id)
									.FirstOrDefaultAsync(cancellationToken);
		}
	}
}