using MediatR;

namespace Application.Tags.Commands;

public class UpdateTagCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}