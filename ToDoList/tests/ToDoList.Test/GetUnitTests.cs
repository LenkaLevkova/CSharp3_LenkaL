namespace ToDoList.Test.UnitTests;

using Microsoft.AspNetCore.Mvc;
using ToDoList.WebApi.Controllers;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using NSubstitute;
using ToDoList.Domain.DTOs;
using Microsoft.AspNetCore.Http;
using NSubstitute.ExceptionExtensions;

public class GetUnitTests
{
    [Fact]
    public void Get_ReadAllAndSomeItemIsAvailable_ReturnsOk()
    {
        // Arrange
        var repository = Substitute.For<IRepository<ToDoItem>>();
        var sampleItems = new List<ToDoItem>
        {
            new ToDoItem { ToDoItemId = 1, Name = "Task 1", Description = "Description 1", IsCompleted = false },
            new ToDoItem { ToDoItemId = 2, Name = "Task 2", Description = "Description 2", IsCompleted = true }
        };

        repository.ReadAll().Returns(sampleItems);
        var controller = new ToDoItemsController(repository);

        // Act
        var result = controller.Read();
        var resultResult = result.Result as OkObjectResult;
        var value = resultResult?.Value as IEnumerable<ToDoItemGetResponseDto>;

        // Assert
        Assert.IsType<OkObjectResult>(resultResult);
        repository.Received(1).ReadAll();
        Assert.NotNull(value);
        Assert.Equal(2, value.Count()); // Ensure that two items are returned
    }

    [Fact]
    public void Get_ReadAllExceptionOccurred_ReturnsInternalServerError()
    {
        // Arrange
        var repository = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repository);
        repository.ReadAll().Throws(new Exception());

        // Act
        var result = controller.Read();
        var resultResult = result.Result;

        // Assert
        Assert.IsType<ObjectResult>(resultResult);
        repository.Received(1).ReadAll();
        Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), resultResult);
    }
}
