using Application.Common.Interfaces;
using DomainLayer.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Commands;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IAppDbContext _context;

    public CreateProductCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Stock = request.Stock,
            CreatedAt = DateTime.UtcNow,
            CategoryId = request.CategoryId,
            Tags = await _context.Tags.Where(t => request.TagIds.Contains(t.Id)).ToListAsync(cancellationToken)
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}