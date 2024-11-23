namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.WebApi.Controllers;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using NSubstitute;
using ToDoList.Domain.DTOs;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;

public class GetUnitTestsHomework
{
    private readonly ToDoItemsController controller;
    private readonly IRepositoryAsync<ToDoItem> repositoryMock;

    public GetUnitTestsHomework()
    {
        repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        controller = new ToDoItemsController(repositoryMock);
    }

    [Fact]
    public async Task Get_ReadWhenSomeItemAvailable_ReturnsOk()
    {
        // Arrange
        var items = new List<ToDoItem>
        {
            new ToDoItem { ToDoItemId = 1, Name = "Test Item 1" },
            new ToDoItem { ToDoItemId = 2, Name = "Test Item 2" }
        };

        repositoryMock.ReadAllAsync().Returns(items);

        // Act
        var result = await controller.ReadAsync();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedItems = Assert.IsAssignableFrom<IEnumerable<ToDoItemGetResponseDto>>(okResult.Value);
        Assert.Equal(items.Count, returnedItems.Count());
        Assert.Equal("Test Item 1", returnedItems.First().Name);
    }

    [Fact]
    public async Task Get_ReadWhenNoItemAvailable_ReturnsNotFound()
    {
        // Arrange
        repositoryMock.ReadAllAsync().Returns(Enumerable.Empty<ToDoItem>());

        // Act
        var result = await controller.ReadAsync();

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task Get_ReadUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        repositoryMock.ReadAllAsync().Throws(new Exception("Unexpected error"));

        // Act
        var result = await controller.ReadAsync();
        var resultResult = result.Result as ObjectResult;
        var problemDetails = resultResult?.Value as ProblemDetails;

        // Assert
        Assert.IsType<ObjectResult>(resultResult);
        Assert.Equal(500, resultResult?.StatusCode);
        Assert.Contains("Unexpected error", problemDetails?.Detail);
    }

    [Fact]
    public async Task Get_ReadByIdWhenSomeItemAvailable_ReturnsOk()
    {
        // Arrange
        var toDoItemId = 1;
        var item = new ToDoItem { ToDoItemId = toDoItemId, Name = "Test Item" };
        repositoryMock.ReadByIdAsync(toDoItemId).Returns(item);

        // Act
        var result = await controller.ReadByIdAsync(toDoItemId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedItem = Assert.IsType<ToDoItemGetResponseDto>(okResult.Value);
        Assert.Equal(toDoItemId, returnedItem.Id);
        Assert.Equal("Test Item", returnedItem.Name);
    }

    [Fact]
    public async Task Get_ReadByIdWhenItemIsNull_ReturnsNotFound()
    {
        // Arrange
        var toDoItemId = 1;
        repositoryMock.ReadByIdAsync(toDoItemId).Returns((ToDoItem)null);

        // Act
        var result = await controller.ReadByIdAsync(toDoItemId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task Get_ReadByIdUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var toDoItemId = 1;
        repositoryMock.ReadByIdAsync(toDoItemId).Throws(new Exception("Unexpected error"));

        // Act
        var result = await controller.ReadByIdAsync(toDoItemId);
        var resultResult = result.Result as ObjectResult;
        var problemDetails = resultResult?.Value as ProblemDetails;

        // Assert
        Assert.IsType<ObjectResult>(resultResult);
        Assert.Equal(500, resultResult?.StatusCode);
        Assert.Contains("Unexpected error", problemDetails?.Detail);
    }
}
