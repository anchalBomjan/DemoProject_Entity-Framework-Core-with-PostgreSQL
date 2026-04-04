using Application.Common.Responses;
using MediatR;

namespace Application.Tags.Delete;

public class DeleteTagCommand : IRequest<Response<bool>>
{
    public int Id { get; set; }
}