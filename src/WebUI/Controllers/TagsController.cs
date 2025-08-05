using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todo_App.Application.Tags.Commands.CreateTag;
using Todo_App.Application.Tags.Commands.DeleteTag;
using Todo_App.Application.Tags.Commands.UpdateTag;
using Todo_App.Application.Tags.Dtos;
using Todo_App.Application.Tags.Queries.GetTags;

namespace Todo_App.WebUI.Controllers;

public class TagsController : ApiControllerBase
{
    private readonly ISender _mediator;

    public TagsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<TagDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<TagDto>>> Get()
    {
        return await _mediator.Send(new GetTagsQuery());
    }

    [HttpPost]
    [ProducesResponseType(typeof(TagDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<TagDto>> Create(CreateTagCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TagDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TagDto>> GetById(int id)
    {
        var tags = await _mediator.Send(new GetTagsQuery());
        var tag = tags.FirstOrDefault(t => t.Id == id);
        if (tag == null)
        {
            return NotFound();
        }
        return tag;
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, UpdateTagCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteTagCommand(id));
        return NoContent();
    }
}
