using MediatR;

namespace Todo_App.Application.Tags.Commands.UpdateTag;
public record UpdateTagCommand(int Id, string Name) : IRequest;
