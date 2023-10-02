using AutoMapper;
using FlashCard.Model;
using MediatR;

namespace FlashCard.Mediator.Words;

public class EditWords
{
	public class Command : IRequest
	{
		public Word Word { get; set; }
	}

	public class Handler : IRequestHandler<Command>
	{
		private readonly FlashCardDbContext _context;
		private readonly IMapper _mapper;

		public Handler(FlashCardDbContext context, IMapper mapper = null)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
		{
			var translation = await _context.Words.FindAsync(request.Word.WordId);

			_mapper.Map(request.Word, translation);

			await _context.SaveChangesAsync();

			return Unit.Value;
		}
	}
}
