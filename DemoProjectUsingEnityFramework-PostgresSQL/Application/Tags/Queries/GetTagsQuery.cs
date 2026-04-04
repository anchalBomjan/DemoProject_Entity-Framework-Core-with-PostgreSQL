using DomainLayer.Entities;
using MediatR;

namespace Application.Tags.Queries;

public class GetTagsQuery : IRequest<IEnumerable<Tag>>
{
    // You can add filtering/pagination parameters here
}