using DomainLayer.Entities;
using MediatR;

namespace Application.Products.Queries;

public class GetProductsQuery : IRequest<IEnumerable<Product>>
{
    // You can add filtering/pagination parameters here
}