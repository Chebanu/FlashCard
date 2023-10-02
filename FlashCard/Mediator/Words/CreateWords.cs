using FlashCard.Model;
using MediatR;

namespace FlashCard.Mediator.Words;

public class CreateWords
{
    public class Command : IRequest
    {
        public Word Word { get; set; }
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
            _context.Words.Add(request.Word);

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
