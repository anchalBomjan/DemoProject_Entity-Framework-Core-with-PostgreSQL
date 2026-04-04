using Application.Common.ModelViews;
using Application.Common.Responses;
using MediatR;

namespace Application.Tags.GetAll;

public class GetTagsQuery : IRequest<Response<IEnumerable<TagViewModel>>>
{
}
