using Application.Common.Interfaces;
using DomainLayer.Entities;
using MediatR;

namespace Application.Tags.Commands;

public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, int>
{
    private readonly IAppDbContext _context;

    public CreateTagCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = new Tag
        {
            Name = request.Name
        };

        _context.Tags.Add(tag);
        await _context.SaveChangesAsync(cancellationToken);

        return tag.Id;
    }
}