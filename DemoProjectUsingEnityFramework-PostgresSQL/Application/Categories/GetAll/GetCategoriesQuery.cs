using Application.Common.ModelViews;
using Application.Common.Responses;
using MediatR;

namespace Application.Categories.GetAll;

public class GetCategoriesQuery : IRequest<Response<IEnumerable<CategoryViewModel>>>
{
}
