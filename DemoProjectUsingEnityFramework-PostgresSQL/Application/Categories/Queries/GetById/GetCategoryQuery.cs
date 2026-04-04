using Application.Common.ModelViews;
using Application.Common.Responses;
using MediatR;

namespace Application.Categories.Queries.GetById;

public class GetCategoryQuery : IRequest<Response<CategoryViewModel?>>
{
    public int Id { get; set; }
}
