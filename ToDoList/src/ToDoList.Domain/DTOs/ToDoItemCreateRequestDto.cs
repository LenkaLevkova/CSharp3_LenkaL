namespace ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;

public record ToDoItemCreateRequestDto(string Name, string Description, bool IsCompleted)
{
    public ToDoItem ToDomain() => new ToDoItem
    {
        Name = this.Name,
        Description = this.Description,
        IsCompleted = this.IsCompleted,
    };
}
