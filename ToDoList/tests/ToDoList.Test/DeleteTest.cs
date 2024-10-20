namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;

public class DeleteTest
{
    private readonly ToDoItemsController controller;

    public DeleteTest()
    {
        controller = new ToDoItemsController();

        var items = new List<ToDoItem>
        {
            new ToDoItem { ToDoItemId = 1, Name = "Item 1", Description = "Description 1", IsCompleted = false },
            new ToDoItem { ToDoItemId = 2, Name = "Item 2", Description = "Description 2", IsCompleted = true }
        };

        ToDoItemsController.items.AddRange(items);
    }

    [Fact]
    public void DeleteById_ItemExists_ShouldDeleteSuccessfully()
    {
        // Arrange
        var toDoItemId = 1;

        // Act
        var result = controller.DeleteById(toDoItemId);

        // Assert
        Assert.IsType<NoContentResult>(result);
        Assert.DoesNotContain(ToDoItemsController.items, i => i.ToDoItemId == toDoItemId);
        Assert.Single(ToDoItemsController.items);
    }

    [Fact]
    public void DeleteById_ItemDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var toDoItemId = 23;

        // Act
        var result = controller.DeleteById(toDoItemId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
