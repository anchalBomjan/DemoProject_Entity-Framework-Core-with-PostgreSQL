using DomainLayer.Entities;
using MediatR;

namespace Application.Categories.Queries;

public class GetCategoriesQuery : IRequest<IEnumerable<Category>>
{
    // You can add filtering/pagination parameters here
}