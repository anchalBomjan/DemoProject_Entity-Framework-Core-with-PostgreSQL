using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.ModelViews;
using Application.Common.Responses;
using DomainLayer.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories.GetById;

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, Response<CategoryViewModel?>>
{
    private readonly IAppDbContext _context;

    public GetCategoryQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Response<CategoryViewModel?>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (category == null)
        {
            throw new NotFoundException(nameof(Category), request.Id);
        }

        var viewModel = new CategoryViewModel
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            ProductsCount = category.Products.Count
        };

        return Response<CategoryViewModel?>.Success(viewModel, "Category retrieved successfully");
    }
}
