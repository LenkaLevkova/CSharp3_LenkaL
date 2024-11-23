namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.WebApi.Controllers;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using NSubstitute;
using ToDoList.Domain.DTOs;

public class DeleteUnitTestsHomework
{
    private readonly IRepositoryAsync<ToDoItem> repositoryMock;
    private readonly ToDoItemsController controller;

    public DeleteUnitTestsHomework()
    {
        repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        controller = new ToDoItemsController(repositoryMock);
    }

    [Fact]
    public async Task Delete_DeleteByIdValidItemId_ReturnsNoContent()
    {
        // Arrange
        var toDoItemId = 1;
        var existingItem = new ToDoItem { ToDoItemId = toDoItemId, Name = "Existing Item" };
        repositoryMock.ReadByIdAsync(toDoItemId).Returns(existingItem);

        // Act
        var result = await controller.DeleteByIdAsync(toDoItemId);

        // Assert
        Assert.IsType<NoContentResult>(result);
        await repositoryMock.Received(1).DeleteAsync(existingItem);
    }

    [Fact]
    public async Task Delete_DeleteByIdInvalidItemId_ReturnsNotFound()
    {
        // Arrange
        var toDoItemId = 1;
        repositoryMock.ReadByIdAsync(toDoItemId).Returns((ToDoItem)null);

        // Act
        var result = await controller.DeleteByIdAsync(toDoItemId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
        await repositoryMock.DidNotReceive().DeleteAsync(Arg.Any<ToDoItem>());
    }

    [Fact]
    public async Task Delete_DeleteByIdUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var toDoItemId = 1;
        var existingItem = new ToDoItem { ToDoItemId = toDoItemId, Name = "Existing Item" };

        repositoryMock.ReadByIdAsync(toDoItemId).Returns(existingItem);
        repositoryMock.When(repo => repo.DeleteAsync(existingItem))
                      .Do(_ => throw new Exception("Unexpected error"));

        // Act
        var result = await controller.DeleteByIdAsync(toDoItemId);
        var resultResult = result as ObjectResult;
        var problemDetails = resultResult?.Value as ProblemDetails;

        // Assert
        Assert.IsType<ObjectResult>(resultResult);
        Assert.Equal(500, resultResult?.StatusCode);
        Assert.Contains("Unexpected error", problemDetails?.Detail);
    }
}
