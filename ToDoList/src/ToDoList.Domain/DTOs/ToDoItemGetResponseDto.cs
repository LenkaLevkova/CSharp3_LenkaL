namespace ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;

public record class ToDoItemGetRequestDto(int Id, string Name, string Description, bool IsCompleted, string? Category = null) //let client know the Id
{
    public static ToDoItemGetRequestDto FromDomain(ToDoItem item) => new(item.ToDoItemId, item.Name, item.Description, item.IsCompleted, item.Category);
}
