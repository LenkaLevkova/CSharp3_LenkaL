namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.WebApi.Controllers;
using ToDoList.Persistence.Repositories;
using ToDoList.Domain.Models;
using NSubstitute;

public class PostTests
{
    [Fact]
    public void Post_ValidRequest_ReturnsNewItem()
    {
        // Arrange
        var repository = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repository);
        var request = new ToDoItemCreateRequestDto(
            Name: "Jmeno",
            Description: "Popis",
            IsCompleted: false
        );

        var domainItem = request.ToDomain();
        repository.When(r => r.Create(Arg.Any<ToDoItem>())).Do(r => domainItem.ToDoItemId = 1);

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
    public void Post_InvalidRequest_Returns500()
    {
        // Arrange
        var repository = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repository);
        var request = new ToDoItemCreateRequestDto(
            Name: "Jmeno",
            Description: "Popis",
            IsCompleted: false
        );

        // Simulate an exception when the repository's Create method is called
        repository.When(r => r.Create(Arg.Any<ToDoItem>())).Do(r => throw new Exception("Database error"));

        // Act
        var result = controller.Create(request);
        var resultResult = result.Result as ObjectResult;

        // Assert
        Assert.IsType<ObjectResult>(resultResult);
        Assert.Equal(500, resultResult?.StatusCode);
    }
}
