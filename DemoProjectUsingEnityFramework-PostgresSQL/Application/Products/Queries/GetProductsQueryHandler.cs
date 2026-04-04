using Application.Common.Interfaces;
using DomainLayer.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Queries;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
{
    private readonly IAppDbContext _context;

    public GetProductsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Tags)
            .ToListAsync(cancellationToken);
    }
}