using MediatR;
using Todo_App.Application.Tags.Dtos;

namespace Todo_App.Application.Tags.Commands.CreateTag;
public class CreateTagCommand : IRequest<TagDto>
{    public string Name { get; set; } = string.Empty;
}