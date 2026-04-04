using Application.Common.DTOs;
using Application.Common.Responses;
using MediatR;

namespace Application.Tags.Create;

public class CreateTagCommand : IRequest<Response<int>>
{
    public TagDto Tag { get; set; } = new TagDto();
}