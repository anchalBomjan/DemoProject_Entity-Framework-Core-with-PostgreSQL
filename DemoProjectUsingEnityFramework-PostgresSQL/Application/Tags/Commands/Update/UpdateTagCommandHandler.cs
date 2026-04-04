using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Responses;
using DomainLayer.Entities;
using MediatR;

namespace Application.Tags.Commands.Update;

public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, Response<bool>>
{
    private readonly IAppDbContext _context;

    public UpdateTagCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Response<bool>> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await _context.Tags.FindAsync(new object[] { request.Id }, cancellationToken);

        if (tag == null)
        {
            throw new NotFoundException(nameof(Tag), request.Id);
        }

        tag.Name = request.Tag.Name;

        await _context.SaveChangesAsync(cancellationToken);

        return Response<bool>.Success(true, "Tag updated successfully");
    }
}
