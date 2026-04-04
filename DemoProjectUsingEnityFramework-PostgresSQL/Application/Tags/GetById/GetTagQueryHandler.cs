using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.ModelViews;
using Application.Common.Responses;
using DomainLayer.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Tags.GetById;

public class GetTagQueryHandler : IRequestHandler<GetTagQuery, Response<TagViewModel?>>
{
    private readonly IAppDbContext _context;

    public GetTagQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Response<TagViewModel?>> Handle(GetTagQuery request, CancellationToken cancellationToken)
    {
        var tag = await _context.Tags
            .Include(t => t.Products)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (tag == null)
        {
            throw new NotFoundException(nameof(Tag), request.Id);
        }

        var tagViewModel = new TagViewModel
        {
            Id = tag.Id,
            Name = tag.Name,
            ProductsCount = tag.Products.Count
        };

        return Response<TagViewModel?>.Success(tagViewModel, "Tag retrieved successfully");
    }
}
