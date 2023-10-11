using AutoMapper;
using FlashCard.Model;
using FlashCard.Model.DTO.TranslationDto;
using FlashCard.Shared.Services.Translations;
using MediatR;

namespace FlashCard.Mediator.Translations;

public class EditTranslations
{
	public class Command : IRequest
	{
		public Guid TranslationId { get; set; }
		public IMediator Mediator { get; set; }
		public TranslationUpdateRequest TranslationUpdateRequest { get; set; }
	}

	public class Handler : IRequestHandler<Command>
	{
		private readonly FlashCardDbContext _context;
		private readonly IMapper _mapper;

		public Handler(FlashCardDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
		{
			var translation = await _context.Translations.FindAsync(request.TranslationId);

			if (translation == null)
				throw new Exception("Translation not found");


			translation = request.TranslationUpdateRequest.ToTranslation();

			var isExist = await TranslationCheker.CheckIfTranslationExists(translation, request.Mediator);

			/*_mapper.Map()
			Temporarily doesn't work, fix soon*/


			if (isExist)
				throw new Exception("Translation can be the same as previous");

			await _context.SaveChangesAsync();

			return Unit.Value;
		}
	}
}
