using Application.Common.DTOs;
using Application.Common.Responses;
using MediatR;

namespace Application.Products.Commands.Create;

public class CreateProductCommand : IRequest<Response<int>>
{
    public ProductDto Product { get; set; } = new ProductDto();
}
