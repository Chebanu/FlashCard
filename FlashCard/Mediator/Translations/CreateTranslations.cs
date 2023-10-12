using AutoMapper;
using FlashCard.Model;
using FlashCard.Model.DTO.TranslationDto;
using FlashCard.Shared.Services.Translations;
using MediatR;

namespace FlashCard.Mediator.Translations;

public class CreateTranslations
{
	public class Command : IRequest
	{
		public IMediator Mediator { get; set; }
		public TranslationRequest TranslationRequest { get; set; }
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
			if (request.TranslationRequest == null && request.Mediator == null)
				throw new ArgumentNullException($"{nameof(request)} or/and {nameof(request)} are empty");

			var translation = _mapper.Map<Translation>(request.TranslationRequest);

			var isExist = await TranslationChecker.CheckIfTranslationExists(translation, request.Mediator);

			if (isExist)
				throw new Exception("The translation is already exist");

			translation.TranslationId = Guid.NewGuid();

			_context.Translations.Add(translation);
			await _context.SaveChangesAsync();

			return Unit.Value;
		}
	}
}
