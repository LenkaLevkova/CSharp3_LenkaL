namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;

public class CreateTest
{
    [Fact]
    public void Create_Item_Successful()
    {
        // // Arrange
        // var controller = new ToDoItemsController();
        // var requestDto = new ToDoItemCreateRequestDto("Jmeno", "Popis", false);

        // var expectedItem = new ToDoItem
        // {
        //     Name = requestDto.Name,
        //     Description = requestDto.Description,
        //     IsCompleted = requestDto.IsCompleted
        // };

        // // Act
        // var result = controller.Create(requestDto);
        // var resultResult = result.Result;
        // var value = result.GetValue();
        // var addedItem = controller.items.First();

        // // Assert
        // Assert.NotNull(value);
        // Assert.IsType<CreatedAtActionResult>(resultResult);
        // Assert.Single(ToDoItemsController.items);
        // Assert.Equal(expectedItem.Name, addedItem.Name);
        // Assert.Equal(expectedItem.Description, addedItem.Description);
        // Assert.Equal(expectedItem.IsCompleted, addedItem.IsCompleted);
    }
}
