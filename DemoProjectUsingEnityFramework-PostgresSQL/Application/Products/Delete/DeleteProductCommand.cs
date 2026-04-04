using Application.Common.Responses;
using MediatR;

namespace Application.Products.Delete;

public class DeleteProductCommand : IRequest<Response<bool>>
{
    public int Id { get; set; }
}
