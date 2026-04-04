using Application.Common.Interfaces;
using DomainLayer.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Tags.Queries;

public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, IEnumerable<Tag>>
{
    private readonly IAppDbContext _context;

    public GetTagsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Tag>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Tags
            .Include(t => t.Products)
            .ToListAsync(cancellationToken);
    }
}