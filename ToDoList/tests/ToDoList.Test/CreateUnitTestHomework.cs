namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.WebApi.Controllers;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using NSubstitute;
using ToDoList.Domain.DTOs;

public class CreateUnitTestHomework
{
    private readonly IRepository<ToDoItem> repositoryMock;
    private readonly ToDoItemsController controller;

    public CreateUnitTestHomework()
    {
        repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        controller = new ToDoItemsController(repositoryMock);
    }

    [Fact]
    public void Post_CreateValidRequest_ReturnsCreatedAtAction()
    {
        // Arrange
        var request = new ToDoItemCreateRequestDto("New Item", "Test Description", false);
        var createdItem = new ToDoItem { ToDoItemId = 1, Name = request.Name, Description = request.Description };
        var domainItem = request.ToDomain();
        repositoryMock.When(r => r.Create(Arg.Any<ToDoItem>())).Do(r => domainItem.ToDoItemId = 1);

        // Act
        var result = controller.Create(request);
        var resultResult = result.Result as CreatedAtActionResult;
        var value = resultResult?.Value as ToDoItemGetResponseDto;

        // Assert
        Assert.IsType<CreatedAtActionResult>(resultResult);
        Assert.NotNull(value);
        Assert.Equal(request.Description, value.Description);
        Assert.Equal(request.IsCompleted, value.IsCompleted);
        Assert.Equal(request.Name, value.Name);
    }

    [Fact]
    public void Post_CreateUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var repository = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repository);
        var request = new ToDoItemCreateRequestDto(
            Name: "Jmeno",
            Description: "Popis",
            IsCompleted: false
        );

        repository.When(r => r.Create(Arg.Any<ToDoItem>())).Do(r => throw new Exception("Error"));

        // Act
        var result = controller.Create(request);
        var resultResult = result.Result as ObjectResult;
        var problemDetails = resultResult?.Value as ProblemDetails;

        // Assert
        Assert.IsType<ObjectResult>(resultResult);
        Assert.Equal(500, resultResult?.StatusCode);
        Assert.Contains("Error", problemDetails?.Detail);
    }
}
