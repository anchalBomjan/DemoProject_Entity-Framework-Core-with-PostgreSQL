using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.ModelViews;
using Application.Common.Responses;
using DomainLayer.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.GetById;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Response<ProductViewModel?>>
{
    private readonly IAppDbContext _context;

    public GetProductQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Response<ProductViewModel?>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Tags)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product == null)
        {
            throw new NotFoundException(nameof(Product), request.Id);
        }

        var viewModel = new ProductViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt,
            CategoryName = product.Category.Name,
            Tags = product.Tags.Select(t => t.Name).ToList()
        };

        return Response<ProductViewModel?>.Success(viewModel, "Product retrieved successfully");
    }
}
