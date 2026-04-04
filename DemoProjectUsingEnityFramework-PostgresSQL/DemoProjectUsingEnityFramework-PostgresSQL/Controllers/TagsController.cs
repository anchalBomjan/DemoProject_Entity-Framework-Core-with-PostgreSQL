using Application.Tags.Commands;
using Application.Tags.Queries;
using DomainLayer.Entities;
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
    public async Task<ActionResult<IEnumerable<Tag>>> GetTags()
    {
        var tags = await _mediator.Send(new GetTagsQuery());
        return Ok(tags);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Tag>> GetTag(int id)
    {
        var tag = await _mediator.Send(new GetTagQuery { Id = id });
        if (tag == null)
        {
            return NotFound();
        }
        return Ok(tag);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateTag(CreateTagCommand command)
    {
        var tagId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetTag), new { id = tagId }, tagId);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTag(int id, UpdateTagCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTag(int id)
    {
        await _mediator.Send(new DeleteTagCommand { Id = id });
        return NoContent();
    }
}