using Application.Common.Interfaces;
using Application.Common.ModelViews;
using Application.Common.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories.Queries.GetAll;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, Response<IEnumerable<CategoryViewModel>>>
{
    private readonly IAppDbContext _context;

    public GetCategoriesQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Response<IEnumerable<CategoryViewModel>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _context.Categories
            .Include(c => c.Products)
            .Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                ProductsCount = c.Products.Count
            })
            .ToListAsync(cancellationToken);

        return Response<IEnumerable<CategoryViewModel>>.Success(categories, "Categories retrieved successfully");
    }
}
