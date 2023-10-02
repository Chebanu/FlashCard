using AutoMapper;
using FlashCard.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlashCard.Translations;

public class Edit
{
	public class Command : IRequest
	{
		public Translation Translation { get; set; }
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
			var translation = await _context.Translations.FindAsync(request.Translation.TranslationId);

			_mapper.Map(request.Translation, translation);

			await _context.SaveChangesAsync();

			return Unit.Value;
		}
	}
}
