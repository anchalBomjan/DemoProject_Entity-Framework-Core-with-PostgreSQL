using Application.Common.DTOs;
using Application.Common.Responses;
using MediatR;

namespace Application.Categories.Update;

public class UpdateCategoryCommand : IRequest<Response<bool>>
{
    public int Id { get; set; }
    public CategoryDto Category { get; set; } = new CategoryDto();
}
