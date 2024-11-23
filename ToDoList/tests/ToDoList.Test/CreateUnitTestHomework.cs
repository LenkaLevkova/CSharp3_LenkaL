namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.WebApi.Controllers;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using NSubstitute;
using ToDoList.Domain.DTOs;

public class CreateUnitTestHomework
{
    private readonly IRepositoryAsync<ToDoItem> repositoryMock;
    private readonly ToDoItemsController controller;

    public CreateUnitTestHomework()
    {
        repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        controller = new ToDoItemsController(repositoryMock);
    }

    [Fact]
    public async Task Post_CreateValidRequest_ReturnsCreatedAtAction()
    {
        // Arrange
        var request = new ToDoItemCreateRequestDto("New Item", "Test Description", false);
        var createdItem = new ToDoItem { ToDoItemId = 1, Name = request.Name, Description = request.Description };
        var domainItem = request.ToDomain();
        repositoryMock.When(r => r.CreateAsync(Arg.Any<ToDoItem>())).Do(r => domainItem.ToDoItemId = 1);

        // Act
        var result = await controller.CreateAsync(request);
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
    public async Task Post_CreateUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        //var repository = Substitute.For<IRepositoryAsync<ToDoItem>>();
        //var controller = new ToDoItemsController(repositoryMock);
        var request = new ToDoItemCreateRequestDto(
            Name: "Jmeno",
            Description: "Popis",
            IsCompleted: false
        );

        repositoryMock.When(r => r.CreateAsync(Arg.Any<ToDoItem>())).Do(r => throw new Exception("Error"));

        // Act
        var result = await controller.CreateAsync(request);
        var resultResult = result.Result as ObjectResult;
        var problemDetails = resultResult?.Value as ProblemDetails;

        // Assert
        Assert.IsType<ObjectResult>(resultResult);
        Assert.Equal(500, resultResult?.StatusCode);
        Assert.Contains("Error", problemDetails?.Detail);
    }
}
