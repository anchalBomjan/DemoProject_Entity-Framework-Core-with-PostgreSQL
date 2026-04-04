using Application.Common.ModelViews;
using Application.Common.Responses;
using Application.Tags.Create;
using Application.Tags.Delete;
using Application.Tags.GetAll;
using Application.Tags.GetById;
using Application.Tags.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DemoProjectUsingEnityFramework_PostgresSQL.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TagsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TagsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<Response<IEnumerable<TagViewModel>>>> GetTags()
    {
        var response = await _mediator.Send(new GetTagsQuery());
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Response<TagViewModel?>>> GetTag(int id)
    {
        var response = await _mediator.Send(new GetTagQuery { Id = id });
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<int>>> CreateTag(CreateTagCommand command)
    {
        var response = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetTag), new { id = response.Data }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Response<bool>>> UpdateTag(int id, UpdateTagCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(Response<bool>.Failure("URL ID does not match command ID"));
        }

        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Response<bool>>> DeleteTag(int id)
    {
        var response = await _mediator.Send(new DeleteTagCommand { Id = id });
        return Ok(response);
    }
}
