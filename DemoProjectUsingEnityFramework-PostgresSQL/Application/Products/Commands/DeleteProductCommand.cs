using MediatR;

namespace Application.Products.Commands;

public class DeleteProductCommand : IRequest
{
    public int Id { get; set; }
}