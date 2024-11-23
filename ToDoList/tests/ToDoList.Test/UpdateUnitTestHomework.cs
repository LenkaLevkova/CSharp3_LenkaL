namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.WebApi.Controllers;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using NSubstitute;
using ToDoList.Domain.DTOs;

public class UpdateUnitTestHomework
{
    private readonly IRepositoryAsync<ToDoItem> repositoryMock;
    private readonly ToDoItemsController controller;

    public UpdateUnitTestHomework()
    {
        repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        controller = new ToDoItemsController(repositoryMock);
    }

    [Fact]
    public async Task Put_UpdateByIdWhenItemUpdated_ReturnsNoContent()
    {
        // Arrange
        var toDoItemId = 1;
        var existingItem = new ToDoItem { ToDoItemId = toDoItemId, Name = "Existing Item" };
        var request = new ToDoItemUpdateRequestDto("Updated Item", "Updated Description", false);
        var updatedItem = new ToDoItem { ToDoItemId = toDoItemId, Name = request.Name, Description = request.Description };
        repositoryMock.ReadByIdAsync(toDoItemId).Returns(existingItem);
        request.ToDomain();

        // Act
        var result = await controller.UpdateByIdAsync(toDoItemId, request);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Put_UpdateByIdWhenIdNotFound_ReturnsNotFound()
    {
        // Arrange
        var toDoItemId = 1;
        var request = new ToDoItemUpdateRequestDto("Updated Item", "Updated Description", false);
        repositoryMock.ReadByIdAsync(toDoItemId).Returns((ToDoItem)null);

        // Act
        var result = await controller.UpdateByIdAsync(toDoItemId, request);

        // Assert
        Assert.IsType<NotFoundResult>(result);
        await repositoryMock.DidNotReceive().UpdateAsync(Arg.Any<ToDoItem>());
    }

    [Fact]
    public async Task Put_UpdateByIdUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var toDoItemId = 1;
        var request = new ToDoItemUpdateRequestDto("Updated Item", "Updated Description", false);
        var existingItem = new ToDoItem { ToDoItemId = toDoItemId, Name = "Existing Item" };
        repositoryMock.ReadByIdAsync(toDoItemId).Returns(existingItem);
        repositoryMock.When(repo => repo.UpdateAsync(Arg.Any<ToDoItem>()))
                      .Do(_ => throw new Exception("Unexpected error"));

        var result = await controller.UpdateByIdAsync(toDoItemId, request);
        var resultResult = result as ObjectResult;
        var problemDetails = resultResult?.Value as ProblemDetails;

        // Assert
        Assert.IsType<ObjectResult>(resultResult);
        Assert.Equal(500, resultResult.StatusCode);
        Assert.Contains("Unexpected error", problemDetails?.Detail);
    }
}
