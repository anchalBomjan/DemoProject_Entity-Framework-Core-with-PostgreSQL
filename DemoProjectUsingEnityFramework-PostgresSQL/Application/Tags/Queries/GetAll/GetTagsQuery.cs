using Application.Common.ModelViews;
using Application.Common.Responses;
using MediatR;

namespace Application.Tags.Queries.GetAll;

public class GetTagsQuery : IRequest<Response<IEnumerable<TagViewModel>>>
{
}
