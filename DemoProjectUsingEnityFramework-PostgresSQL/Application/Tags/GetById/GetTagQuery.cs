using Application.Common.ModelViews;
using Application.Common.Responses;
using MediatR;

namespace Application.Tags.GetById;

public class GetTagQuery : IRequest<Response<TagViewModel?>>
{
    public int Id { get; set; }
}
