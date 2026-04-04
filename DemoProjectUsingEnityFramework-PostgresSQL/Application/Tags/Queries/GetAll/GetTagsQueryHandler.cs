using Application.Common.Interfaces;
using Application.Common.ModelViews;
using Application.Common.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Tags.Queries.GetAll;

public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, Response<IEnumerable<TagViewModel>>>
{
    private readonly IAppDbContext _context;

    public GetTagsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Response<IEnumerable<TagViewModel>>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
    {
        var tags = await _context.Tags
            .Include(t => t.Products)
            .Select(t => new TagViewModel
            {
                Id = t.Id,
                Name = t.Name,
                ProductsCount = t.Products.Count
            })
            .ToListAsync(cancellationToken);

        return Response<IEnumerable<TagViewModel>>.Success(tags, "Tags retrieved successfully");
    }
}
