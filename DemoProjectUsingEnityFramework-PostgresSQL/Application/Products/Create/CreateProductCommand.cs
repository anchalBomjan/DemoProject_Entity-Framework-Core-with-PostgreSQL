using Application.Common.DTOs;
using Application.Common.Responses;
using MediatR;

namespace Application.Products.Create;

public class CreateProductCommand : IRequest<Response<int>>
{
    public ProductDto Product { get; set; } = new ProductDto();
}
