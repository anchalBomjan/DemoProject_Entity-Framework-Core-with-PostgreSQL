using Application.Common.Interfaces;
using DomainLayer.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Queries;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Product?>
{
    private readonly IAppDbContext _context;

    public GetProductQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Tags)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
    }
}