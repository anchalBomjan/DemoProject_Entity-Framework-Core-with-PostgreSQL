using Application.Common.ModelViews;
using Application.Common.Responses;
using MediatR;

namespace Application.Products.GetById;

public class GetProductQuery : IRequest<Response<ProductViewModel?>>
{
    public int Id { get; set; }
}
