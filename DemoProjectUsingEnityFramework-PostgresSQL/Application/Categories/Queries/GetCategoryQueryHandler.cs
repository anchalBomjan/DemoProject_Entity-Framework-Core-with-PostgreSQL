using Application.Common.Interfaces;
using DomainLayer.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories.Queries;

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, Category?>
{
    private readonly IAppDbContext _context;

    public GetCategoryQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Category?> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        return await _context.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
    }
}