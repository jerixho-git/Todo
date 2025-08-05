using MediatR;

namespace Todo_App.Application.Tags.Commands.DeleteTag;
public record DeleteTagCommand(int Id) : IRequest;
