using Application.Common.ModelViews;
using Application.Common.Responses;
using MediatR;

namespace Application.Products.GetAll;

public class GetProductsQuery : IRequest<Response<IEnumerable<ProductViewModel>>>
{
}
