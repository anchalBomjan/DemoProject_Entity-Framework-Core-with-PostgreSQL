using MediatR;

namespace Application.Tags.Commands;

public class DeleteTagCommand : IRequest
{
    public int Id { get; set; }
}