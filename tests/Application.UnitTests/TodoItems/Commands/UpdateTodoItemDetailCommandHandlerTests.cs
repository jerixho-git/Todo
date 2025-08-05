using FluentAssertions;
using Moq;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Todo_App.Application.TodoItems.Commands.UpdateTodoItemDetail;
using Todo_App.Domain.Entities;
using Todo_App.Domain.Enums;
using Todo_App.Application.Common.Interfaces;
using Todo_App.Domain.ValueObjects;

namespace Todo_App.Application.UnitTests.TodoItems.Commands;

internal class UpdateTodoItemDetailCommandHandlerTests
{
    [Test]
    public async Task ShouldUpdateBackgroundColor()
    {
        var item = new TodoItem { Id = 1, ListId = 1, BackgroundColor = Colour.White };
        var context = CreateMockContextWithItem(item);

        var handler = new UpdateTodoItemDetailCommandHandler(context);
        var command = new UpdateTodoItemDetailCommand
        {
            Id = 1,
            ListId = 1,
            Colour = "#FFFFFF",
            Priority = PriorityLevel.None
        };

        await handler.Handle(command, default);

        item.BackgroundColor.Should().NotBeNull();
        item.BackgroundColor!.Code.Should().Be("#FFFFFF");
    }

    private IApplicationDbContext CreateMockContextWithItem(TodoItem item)
    {
        var mockSet = new Mock<DbSet<TodoItem>>();

        var mockContext = new Mock<IApplicationDbContext>();

        mockContext.Setup(c => c.TodoItems.FindAsync(new object[] { item.Id }, It.IsAny<CancellationToken>()))
                   .ReturnsAsync(item);

        return mockContext.Object;
    }

}
