using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Responses;
using DomainLayer.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Commands.Update;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response<bool>>
{
    private readonly IAppDbContext _context;

    public UpdateProductCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Response<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .Include(p => p.Tags)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product == null)
        {
            throw new NotFoundException(nameof(Product), request.Id);
        }

        product.Name = request.Product.Name;
        product.Description = request.Product.Description;
        product.Price = request.Product.Price;
        product.Stock = request.Product.Stock;
        product.UpdatedAt = DateTime.UtcNow;
        product.CategoryId = request.Product.CategoryId;
        product.Tags = await _context.Tags
            .Where(t => request.Product.TagIds.Contains(t.Id))
            .ToListAsync(cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return Response<bool>.Success(true, "Product updated successfully");
    }
}
