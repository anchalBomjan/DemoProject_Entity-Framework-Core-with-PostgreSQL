using Application.Categories.Commands.Create;
using Application.Categories.Commands.Delete;
using Application.Categories.Commands.Update;
using Application.Categories.Queries.GetAll;
using Application.Categories.Queries.GetById;
using Application.Common.ModelViews;
using Application.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DemoProjectUsingEnityFramework_PostgresSQL.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<Response<IEnumerable<CategoryViewModel>>>> GetCategories()
    {
        var response = await _mediator.Send(new GetCategoriesQuery());
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Response<CategoryViewModel?>>> GetCategory(int id)
    {
        var response = await _mediator.Send(new GetCategoryQuery { Id = id });
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<int>>> CreateCategory(CreateCategoryCommand command)
    {
        var response = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetCategory), new { id = response.Data }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Response<bool>>> UpdateCategory(int id, UpdateCategoryCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(Application.Common.Responses.Response<bool>.Failure("URL ID does not match command ID"));
        }

        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Response<bool>>> DeleteCategory(int id)
    {
        var response = await _mediator.Send(new DeleteCategoryCommand { Id = id });
        return Ok(response);
    }
}
