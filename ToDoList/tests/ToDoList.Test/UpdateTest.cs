namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;

public class UpdateTest
{
    [Fact]
    public void Update_OneItem_UpdatedById()
    {
        // // Arrange
        // var controller = new ToDoItemsController();
        // var toDoItem = new ToDoItem
        // {
        //     ToDoItemId = 1,
        //     Name = "Jmeno",
        //     Description = "Popis",
        //     IsCompleted = false
        // };
        // ToDoItemsController.items.Add(toDoItem);

        // var toDoItemId = 1;
        // var updateRequestDto = new ToDoItemUpdateRequestDto("Upravene jmeno", "Upraveny popis", true);

        // var expectedUpdatedItem = new ToDoItem
        // {
        //     ToDoItemId = toDoItemId,
        //     Name = updateRequestDto.Name,
        //     Description = updateRequestDto.Description,
        //     IsCompleted = updateRequestDto.IsCompleted
        // };

        // // Act
        // var result = controller.UpdateById(toDoItemId, updateRequestDto);
        // var updatedItem = ToDoItemsController.items.First(i => i.ToDoItemId == toDoItemId);

        // // Assert
        // Assert.IsType<NoContentResult>(result);
        // Assert.Equal(expectedUpdatedItem.Name, updatedItem.Name);
        // Assert.Equal(expectedUpdatedItem.Description, updatedItem.Description);
        // Assert.Equal(expectedUpdatedItem.IsCompleted, updatedItem.IsCompleted);
        // Assert.Equal(expectedUpdatedItem.ToDoItemId, updatedItem.ToDoItemId);
    }
}
