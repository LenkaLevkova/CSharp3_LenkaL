namespace ToDoList.Domain.DTOs;

using ToDoList.Domain.Models;

public record ToDoItemCreateRequestDto(string Name, string Description, bool IsCompleted, string? Category = null)
{
    public ToDoItem ToDomain() => new ToDoItem
    {
        Name = this.Name,
        Description = this.Description,
        IsCompleted = this.IsCompleted,
        Category = this.Category
    };
}
