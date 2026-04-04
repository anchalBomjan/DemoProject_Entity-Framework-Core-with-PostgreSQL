using Application.Common.Responses;
using MediatR;

namespace Application.Categories.Commands.Delete;

public class DeleteCategoryCommand : IRequest<Response<bool>>
{
    public int Id { get; set; }
}
