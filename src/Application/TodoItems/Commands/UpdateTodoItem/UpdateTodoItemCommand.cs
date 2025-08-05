using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo_App.Application.Common.Exceptions;
using Todo_App.Application.Common.Interfaces;
using Todo_App.Domain.Entities;
using Todo_App.Domain.ValueObjects;

namespace Todo_App.Application.TodoItems.Commands.UpdateTodoItem;

public record UpdateTodoItemCommand : IRequest
{
    public int Id { get; init; }

    public string? Title { get; init; }

    public bool Done { get; init; }
    public string? BackgroundColor { get; init; }
    public List<int> TagIds { get; init; } = new();
}

public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoItems
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(TodoItem), request.Id);
        }

        entity.Title = request.Title;
        entity.Done = request.Done;
        entity.BackgroundColor = Colour.From(request.BackgroundColor ?? Colour.White);

        var tags = await _context.Tags
       .Where(tag => request.TagIds.Contains(tag.Id))
       .ToListAsync(cancellationToken);

        entity.Tags.Clear();
        foreach (var tag in tags)
        {
            entity.Tags.Add(tag);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
