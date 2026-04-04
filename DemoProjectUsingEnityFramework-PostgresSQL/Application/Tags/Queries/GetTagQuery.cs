using DomainLayer.Entities;
using MediatR;

namespace Application.Tags.Queries;

public class GetTagQuery : IRequest<Tag?>
{
    public int Id { get; set; }
}