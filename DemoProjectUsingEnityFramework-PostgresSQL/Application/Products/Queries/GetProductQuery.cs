using DomainLayer.Entities;
using MediatR;

namespace Application.Products.Queries;

public class GetProductQuery : IRequest<Product?>
{
    public int Id { get; set; }
}