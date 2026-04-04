using Application.Common.Interfaces;
using MediatR;

namespace Application.Tags.Commands;

public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand>
{
    private readonly IAppDbContext _context;

    public DeleteTagCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await _context.Tags.FindAsync(request.Id, cancellationToken);

        if (tag == null)
        {
            throw new KeyNotFoundException($"Tag with ID {request.Id} not found.");
        }

        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync(cancellationToken);
    }
}