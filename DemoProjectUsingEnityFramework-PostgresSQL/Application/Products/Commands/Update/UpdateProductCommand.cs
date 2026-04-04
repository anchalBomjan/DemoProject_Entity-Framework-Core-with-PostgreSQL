using Application.Common.DTOs;
using Application.Common.Responses;
using MediatR;

namespace Application.Products.Commands.Update;

public class UpdateProductCommand : IRequest<Response<bool>>
{
    public int Id { get; set; }
    public ProductDto Product { get; set; } = new ProductDto();
}
