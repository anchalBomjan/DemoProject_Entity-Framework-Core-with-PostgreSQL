using DomainLayer.Entities;
using MediatR;

namespace Application.Categories.Queries;

public class GetCategoryQuery : IRequest<Category?>
{
    public int Id { get; set; }
}