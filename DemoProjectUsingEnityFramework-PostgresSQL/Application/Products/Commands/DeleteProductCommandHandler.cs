using Application.Common.Interfaces;
using MediatR;

namespace Application.Products.Commands;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IAppDbContext _context;

    public DeleteProductCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(request.Id, cancellationToken);

        if (product == null)
        {
            throw new KeyNotFoundException($"Product with ID {request.Id} not found.");
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
    }
}