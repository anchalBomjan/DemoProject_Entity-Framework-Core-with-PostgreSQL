using Application.Common.DTOs;
using Application.Common.Responses;
using MediatR;

namespace Application.Categories.Create;

public class CreateCategoryCommand : IRequest<Response<int>>
{
    public CategoryDto Category { get; set; } = new CategoryDto();
}
