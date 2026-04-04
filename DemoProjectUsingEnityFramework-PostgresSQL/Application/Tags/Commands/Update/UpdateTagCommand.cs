using Application.Common.DTOs;
using Application.Common.Responses;
using MediatR;

namespace Application.Tags.Commands.Update;

public class UpdateTagCommand : IRequest<Response<bool>>
{
    public int Id { get; set; }
    public TagDto Tag { get; set; } = new TagDto();
}
