using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Responses;
using DomainLayer.Entities;
using MediatR;

namespace Application.Tags.Commands.Delete;

public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand, Response<bool>>
{
    private readonly IAppDbContext _context;

    public DeleteTagCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Response<bool>> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await _context.Tags.FindAsync(new object[] { request.Id }, cancellationToken);

        if (tag == null)
        {
            throw new NotFoundException(nameof(Tag), request.Id);
        }

        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync(cancellationToken);

        return Response<bool>.Success(true, "Tag deleted successfully");
    }
}
