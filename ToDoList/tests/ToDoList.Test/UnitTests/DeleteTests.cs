namespace ToDoList.Test.UnitTests;

using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using ToDoList.WebApi.Controllers;
using ToDoList.Persistence.Repositories;
using ToDoList.Domain.Models;
using Microsoft.AspNetCore.Http;
using NSubstitute.ExceptionExtensions;

public class DeleteUnitTests
{
    [Fact]
    public async Task Delete_DeleteByIdValidItemId_ReturnsNoContent()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.ReadByIdAsync(Arg.Any<int>()).Returns(new ToDoItem { Name = "testItem", Description = "testDescription", IsCompleted = false });
        var someId = 1;

        // Act
        var result = await controller.DeleteByIdAsync(someId);

        // Assert
        Assert.IsType<NoContentResult>(result);
        await repositoryMock.Received(1).ReadByIdAsync(someId);
        await repositoryMock.Received(1).DeleteAsync(Arg.Any<ToDoItem>());
    }

    [Fact]
    public async Task Delete_DeleteByIdInvalidItemId_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.ReadByIdAsync(Arg.Any<int>()).Returns(null as ToDoItem);
        var someId = 1;

        // Act
        var result = await controller.DeleteByIdAsync(someId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
        await repositoryMock.Received(1).ReadByIdAsync(someId);
        await repositoryMock.Received(0).DeleteAsync(Arg.Any<ToDoItem>()); // make sure nothing was deleted
    }

    [Fact]
    public async Task Delete_DeleteByIdExceptionOccurredDuringRepositoryReadById_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.ReadByIdAsync(Arg.Any<int>()).Throws(new Exception());
        var someId = 1;

        // Act
        var result = await controller.DeleteByIdAsync(someId);

        // Assert
        Assert.IsType<ObjectResult>(result);
        await repositoryMock.Received(1).ReadByIdAsync(someId);
        Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
    }

    [Fact]
    public async Task Delete_DeleteByIdExceptionOccurredDuringRepositoryDelete_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.ReadByIdAsync(Arg.Any<int>()).Returns(new ToDoItem { Name = "testItem", Description = "testDescription", IsCompleted = false });
        repositoryMock.When(r => r.DeleteAsync(Arg.Any<ToDoItem>())).Do(r => throw new Exception());
        var someId = 1;

        // Act
        var result = await controller.DeleteByIdAsync(someId);

        // Assert
        Assert.IsType<ObjectResult>(result);
        await repositoryMock.Received(1).ReadByIdAsync(someId);
        await repositoryMock.Received(1).DeleteAsync(Arg.Any<ToDoItem>());
        Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
    }
}
