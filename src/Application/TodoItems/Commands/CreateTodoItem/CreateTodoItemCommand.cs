using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo_App.Application.Common.Interfaces;
using Todo_App.Domain.Entities;
using Todo_App.Domain.Enums;
using Todo_App.Domain.Events;
using Todo_App.Domain.ValueObjects;

namespace Todo_App.Application.TodoItems.Commands.CreateTodoItem;

public class CreateTodoItemCommand : IRequest<int>
{
    public int ListId { get; set; }
    public string? Title { get; set; }
    public bool Done { get; set; } = false;
    public PriorityLevel Priority { get; set; }
    public string? Note { get; set; }
    public string? Colour { get; set; }
    public List<int> TagIds { get; set; } = new();
}


public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new TodoItem
        {
            ListId = request.ListId,
            Title = request.Title,
            Done = request.Done,
            Priority = request.Priority,
            Note = request.Note,
            BackgroundColor = Colour.From(request.Colour ?? Colour.White)
        };

        if (request.TagIds?.Any() == true)
        {
            var tags = await _context.Tags
                .Where(t => request.TagIds.Contains(t.Id))
                .ToListAsync(cancellationToken);

            foreach (var tag in tags)
            {
                entity.Tags.Add(tag);
            }
        }

        entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

        _context.TodoItems.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
