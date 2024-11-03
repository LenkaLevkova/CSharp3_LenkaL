namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.WebApi.Controllers;
using ToDoList.Persistence.Repositories;
using ToDoList.Domain.Models;
using NSubstitute;

public class PutTests
{
    [Fact]
    public void Put_ValidId_ReturnsNoContent()
    {
        // Arrange
        var repository = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repository);
        var validId = 1;

        var request = new ToDoItemUpdateRequestDto(
            Name: "Jine jmeno",
            Description: "Jiny popis",
            IsCompleted: true
        );

        // Act
        var result = controller.UpdateById(validId, request);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void Put_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var repository = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repository);
        var invalidId = -1;

        var request = new ToDoItemUpdateRequestDto(
            Name: "Jine jmeno",
            Description: "Jiny popis",
            IsCompleted: true
        );

        // Simulate exception for invalid ID or absence of item
        repository.When(r => r.UpdateById(invalidId, Arg.Any<ToDoItem>()))
                  .Do(r => throw new KeyNotFoundException("Item not found"));

        // Act
        var result = controller.UpdateById(invalidId, request);
        var resultResult = result as ObjectResult;
        var problemDetails = resultResult?.Value as ProblemDetails;

        // Assert
        Assert.IsType<ObjectResult>(resultResult);
        Assert.Equal(500, resultResult?.StatusCode);
        Assert.Contains("Item not found", problemDetails?.Detail);
    }
}
