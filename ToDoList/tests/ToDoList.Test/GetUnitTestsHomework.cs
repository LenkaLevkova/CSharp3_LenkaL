namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.WebApi.Controllers;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using NSubstitute;
using ToDoList.Domain.DTOs;

public class GetUnitTestsHomework
{
    private readonly IRepository<ToDoItem> repositoryMock;
    private readonly ToDoItemsController controller;

    public GetUnitTestsHomework()
    {
        repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        controller = new ToDoItemsController(repositoryMock);
    }

    [Fact]
    public void Get_ReadWhenSomeItemAvailable_ReturnsOk()
    {
        // Arrange
        var items = new List<ToDoItem>
        {
            new ToDoItem { ToDoItemId = 1, Name = "Test Item 1" },
            new ToDoItem { ToDoItemId = 2, Name = "Test Item 2" }
        };

        repositoryMock.ReadAll().Returns(items);

        // Act
        var result = controller.Read();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedItems = Assert.IsAssignableFrom<IEnumerable<ToDoItemGetResponseDto>>(okResult.Value);
        Assert.Equal(items.Count, returnedItems.Count());
        Assert.Equal("Test Item 1", returnedItems.First().Name);
    }

    [Fact]
    public void Get_ReadWhenNoItemAvailable_ReturnsNotFound()
    {
        // Arrange
        repositoryMock.ReadAll().Returns(Enumerable.Empty<ToDoItem>());

        // Act
        var result = controller.Read();

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public void Get_ReadUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        repositoryMock.ReadAll().Returns(_ => throw new Exception("Unexpected error"));

        // Act
        var result = controller.Read();
        var resultResult = result.Result as ObjectResult;
        var problemDetails = resultResult?.Value as ProblemDetails;

        // Assert
        Assert.IsType<ObjectResult>(resultResult);
        Assert.Equal(500, resultResult?.StatusCode);
        Assert.Contains("Unexpected error", problemDetails?.Detail);
    }

    [Fact]
    public void Get_ReadByIdWhenSomeItemAvailable_ReturnsOk()
    {
        // Arrange
        var toDoItemId = 1;
        var item = new ToDoItem { ToDoItemId = toDoItemId, Name = "Test Item" };
        repositoryMock.ReadById(toDoItemId).Returns(item);

        // Act
        var result = controller.ReadById(toDoItemId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedItem = Assert.IsType<ToDoItemGetResponseDto>(okResult.Value);
        Assert.Equal(toDoItemId, returnedItem.Id);
        Assert.Equal("Test Item", returnedItem.Name);
    }

    [Fact]
    public void Get_ReadByIdWhenItemIsNull_ReturnsNotFound()
    {
        // Arrange
        var toDoItemId = 1;
        repositoryMock.ReadById(toDoItemId).Returns((ToDoItem)null);

        // Act
        var result = controller.ReadById(toDoItemId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public void Get_ReadByIdUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var toDoItemId = 1;
        repositoryMock.ReadById(toDoItemId).Returns(_ => throw new Exception("Unexpected error"));

        // Act
        var result = controller.ReadById(toDoItemId);
        var resultResult = result.Result as ObjectResult;
        var problemDetails = resultResult?.Value as ProblemDetails;

        // Assert
        Assert.IsType<ObjectResult>(resultResult);
        Assert.Equal(500, resultResult?.StatusCode);
        Assert.Contains("Unexpected error", problemDetails?.Detail);
    }
}
