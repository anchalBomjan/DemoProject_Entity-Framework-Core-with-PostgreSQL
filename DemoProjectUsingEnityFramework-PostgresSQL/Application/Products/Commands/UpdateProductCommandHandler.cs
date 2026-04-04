using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Commands;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IAppDbContext _context;

    public UpdateProductCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .Include(p => p.Tags)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product == null)
        {
            throw new KeyNotFoundException($"Product with ID {request.Id} not found.");
        }

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.Stock = request.Stock;
        product.UpdatedAt = DateTime.UtcNow;
        product.CategoryId = request.CategoryId;
        product.Tags = await _context.Tags.Where(t => request.TagIds.Contains(t.Id)).ToListAsync(cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }
}