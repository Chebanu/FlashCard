using AutoMapper;
using FlashCard.Model;
using FlashCard.Model.DTO.TranslationDto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlashCard.Mediator.Translations;

public class DetailsTranslations
{
	public class Query : IRequest<TranslationResponse>
	{
		public Guid Id { get; set; }
	}
	public class Handler : IRequestHandler<Query, TranslationResponse>
	{
		private readonly FlashCardDbContext _context;
		private readonly IMapper _mapper;

		public Handler(FlashCardDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<TranslationResponse> Handle(Query request, CancellationToken cancellationToken)
		{
			var translation = await _context.Translations
												.Include(t => t.SourceWord)
												.Include(t => t.TargetWord)
												.Include(t => t.SourceWord.Theme.ThemeName)
												.Include(t => t.TargetWord.Theme.ThemeName)
												.Include(s => s.SourceWord.Language)
												.Include(s => s.TargetWord.Language)
												.Include(s => s.SourceWord.Level)
												.Include(s => s.TargetWord.Level)
											.Where(t => t.TranslationId == request.Id)
										.FirstOrDefaultAsync(cancellationToken);

			if (translation == null)
			{
				throw new Exception("The translation doesn't exist");
			}

			var translationResposne = _mapper.Map<TranslationResponse>(translation);

			return translationResposne;
		}

	}
}