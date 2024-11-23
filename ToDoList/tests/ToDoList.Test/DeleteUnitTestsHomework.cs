namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.WebApi.Controllers;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using NSubstitute;
using ToDoList.Domain.DTOs;

public class DeleteUnitTestsHomework
{
    private readonly IRepository<ToDoItem> repositoryMock;
    private readonly ToDoItemsController controller;

    public DeleteUnitTestsHomework()
    {
        repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        controller = new ToDoItemsController(repositoryMock);
    }

    [Fact]
    public void Delete_DeleteByIdValidItemId_ReturnsNoContent()
    {
        // Arrange
        var toDoItemId = 1;
        var existingItem = new ToDoItem { ToDoItemId = toDoItemId, Name = "Existing Item" };
        repositoryMock.ReadById(toDoItemId).Returns(existingItem);

        // Act
        var result = controller.DeleteById(toDoItemId);

        // Assert
        Assert.IsType<NoContentResult>(result);
        repositoryMock.Received(1).Delete(existingItem);
    }

    [Fact]
    public void Delete_DeleteByIdInvalidItemId_ReturnsNotFound()
    {
        // Arrange
        var toDoItemId = 1;
        repositoryMock.ReadById(toDoItemId).Returns((ToDoItem)null);

        // Act
        var result = controller.DeleteById(toDoItemId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
        repositoryMock.DidNotReceive().Delete(Arg.Any<ToDoItem>());
    }

    [Fact]
    public void Delete_DeleteByIdUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var toDoItemId = 1;
        var existingItem = new ToDoItem { ToDoItemId = toDoItemId, Name = "Existing Item" };

        repositoryMock.ReadById(toDoItemId).Returns(existingItem);
        repositoryMock.When(repo => repo.Delete(existingItem))
                      .Do(_ => throw new Exception("Unexpected error"));

        // Act
        var result = controller.DeleteById(toDoItemId);
        var resultResult = result as ObjectResult;
        var problemDetails = resultResult?.Value as ProblemDetails;

        // Assert
        Assert.IsType<ObjectResult>(resultResult);
        Assert.Equal(500, resultResult?.StatusCode);
        Assert.Contains("Unexpected error", problemDetails?.Detail);
    }
}
