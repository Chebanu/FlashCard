using FlashCard.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlashCard.Translations;

public class List
{
    public class Query : IRequest<List<Translation>> { }

    public class Handler : IRequestHandler<Query, List<Translation>>
    {
        public readonly FlashCardDbContext _context;

        public Handler(FlashCardDbContext context)
        {
            _context = context;
        }

        public async Task<List<Translation>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.Translations.Include(t => t.SourceWord)
                                                .Include(l=>l.SourceWord.Level)
                                                .Include(l => l.SourceWord.Language)
                                                .Include(t => t.TargetWord)
                                                .Include(l=> l.TargetWord.Level)
                                                .Include(l => l.TargetWord.Language)
                                            .ToListAsync();
        }
    }
}
