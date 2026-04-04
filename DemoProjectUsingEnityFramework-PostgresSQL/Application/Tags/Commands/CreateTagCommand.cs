using MediatR;

namespace Application.Tags.Commands;

public class CreateTagCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
}