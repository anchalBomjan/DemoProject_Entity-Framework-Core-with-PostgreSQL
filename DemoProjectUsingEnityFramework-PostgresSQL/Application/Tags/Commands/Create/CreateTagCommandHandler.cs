using Application.Common.Interfaces;
using Application.Common.Responses;
using DomainLayer.Entities;
using MediatR;

namespace Application.Tags.Commands.Create;

public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, Response<int>>
{
    private readonly IAppDbContext _context;

    public CreateTagCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Response<int>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = new Tag
        {
            Name = request.Tag.Name
        };

        _context.Tags.Add(tag);
        await _context.SaveChangesAsync(cancellationToken);

        return Response<int>.Success(tag.Id, "Tag created successfully");
    }
}
