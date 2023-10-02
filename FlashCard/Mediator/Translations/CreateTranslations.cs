using FlashCard.Model;
using MediatR;

namespace FlashCard.Mediator.Translations;

public class CreateTranslations
{
    public class Command : IRequest
    {
        public Translation Translation { get; set; }
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly FlashCardDbContext _context;

        public Handler(FlashCardDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            _context.Translations.Add(request.Translation);

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
