using Application.Common.Interfaces;
using Application.Common.ModelViews;
using Application.Common.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.GetAll;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Response<IEnumerable<ProductViewModel>>>
{
    private readonly IAppDbContext _context;

    public GetProductsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Response<IEnumerable<ProductViewModel>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Tags)
            .Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                CategoryName = p.Category.Name,
                Tags = p.Tags.Select(t => t.Name).ToList()
            })
            .ToListAsync(cancellationToken);

        return Response<IEnumerable<ProductViewModel>>.Success(products, "Products retrieved successfully");
    }
}
