using Application.Common.Interfaces;
using DomainLayer.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Tags.Queries;

public class GetTagQueryHandler : IRequestHandler<GetTagQuery, Tag?>
{
    private readonly IAppDbContext _context;

    public GetTagQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Tag?> Handle(GetTagQuery request, CancellationToken cancellationToken)
    {
        return await _context.Tags
            .Include(t => t.Products)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
    }
}