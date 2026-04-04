using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Responses;
using DomainLayer.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Commands.Create;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response<int>>
{
    private readonly IAppDbContext _context;

    public CreateProductCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Response<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.FindAsync(new object[] { request.Product.CategoryId }, cancellationToken);
        if (category == null)
        {
            throw new NotFoundException(nameof(Category), request.Product.CategoryId);
        }

        var product = new Product
        {
            Name = request.Product.Name,
            Description = request.Product.Description,
            Price = request.Product.Price,
            Stock = request.Product.Stock,
            CreatedAt = DateTime.UtcNow,
            CategoryId = request.Product.CategoryId,
            Tags = await _context.Tags
                .Where(t => request.Product.TagIds.Contains(t.Id))
                .ToListAsync(cancellationToken)
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);

        return Response<int>.Success(product.Id, "Product created successfully");
    }
}
