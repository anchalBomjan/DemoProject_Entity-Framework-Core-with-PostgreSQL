using Application.Common.Interfaces;
using MediatR;

namespace Application.Tags.Commands;

public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand>
{
    private readonly IAppDbContext _context;

    public UpdateTagCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await _context.Tags.FindAsync(request.Id, cancellationToken);

        if (tag == null)
        {
            throw new KeyNotFoundException($"Tag with ID {request.Id} not found.");
        }

        tag.Name = request.Name;

        await _context.SaveChangesAsync(cancellationToken);
    }
}