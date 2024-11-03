namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.WebApi.Controllers;
using ToDoList.Persistence.Repositories;
using ToDoList.Domain.Models;
using NSubstitute;

public class GetTests
{
    [Fact]
    public void Get_AllItems_ReturnsAllItems()
    {
        // Arrange
        var repository = Substitute.For<IRepository<ToDoItem>>();
        var sampleItems = new List<ToDoItem>
        {
            new ToDoItem { ToDoItemId = 1, Name = "Task 1", Description = "Description 1", IsCompleted = false },
            new ToDoItem { ToDoItemId = 2, Name = "Task 2", Description = "Description 2", IsCompleted = true }
        };

        repository.Read().Returns(sampleItems);
        var controller = new ToDoItemsController(repository);

        // Act
        var result = controller.Read();
        var resultResult = result.Result as OkObjectResult;
        var value = resultResult?.Value as IEnumerable<ToDoItemGetResponseDto>;

        // Assert
        Assert.IsType<OkObjectResult>(resultResult);
        Assert.NotNull(value);
        Assert.Equal(2, value.Count()); // Ensure that two items are returned
    }

    [Fact]
    public void GetById_ValidId_ReturnsItem()
    {
        // Arrange
        var repository = Substitute.For<IRepository<ToDoItem>>();
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 9,
            Name = "Namegg",
            Description = "Example New Description",
            IsCompleted = false
        };

        repository.ReadById(9).Returns(toDoItem);
        var controller = new ToDoItemsController(repository);

        // Act
        var result = controller.ReadById(9);
        var resultResult = result.Result as OkObjectResult;
        var value = resultResult?.Value as ToDoItemGetResponseDto;

        // Assert
        Assert.IsType<OkObjectResult>(resultResult);
        Assert.NotNull(value);
        Assert.Equal(toDoItem.ToDoItemId, value.Id);
        Assert.Equal(toDoItem.Description, value.Description);
        Assert.Equal(toDoItem.IsCompleted, value.IsCompleted);
        Assert.Equal(toDoItem.Name, value.Name);
    }

    [Fact]
    public void GetById_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var repository = Substitute.For<IRepository<ToDoItem>>();
        repository.ReadById(-1).Returns((ToDoItem)null);
        var controller = new ToDoItemsController(repository);

        // Act
        var result = controller.ReadById(-1);
        var resultResult = result.Result;

        // Assert
        Assert.IsType<NotFoundResult>(resultResult);
    }
}
