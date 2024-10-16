namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.WebApi.Controllers;
using ToDoList.Domain.Models;

public class GetTest
{
    // [Fact]
    // public void Get_AllItems_ReturnsAllItems()
    // {
    //     var controller = new ToDoItemsController();
    //     ToDoItemsController.items.Add(new ToDoItem());
    //     var result = controller.Read();
    //     Assert.True(result is OkObjectResult);
    // }

    [Fact]
    public void Get_AllItems_ReturnsAllItems2()
    {
        // Arrange
        var controller = new ToDoItemsController();
        var toDoItem = new ToDoItem();
        ToDoItemsController.items.Add(toDoItem);
        // Act
        var result = controller.Read();
        var value = result.Value;
        var resultResult = result.Result;
        // Assert
        Assert.True(resultResult is OkObjectResult);
        Assert.IsType<OkObjectResult>(resultResult);
    }
}
