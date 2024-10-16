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

    //Should this be here? If so, where is it appropriate to use it?
    public static ToDoItemCreateRequestDto FromDomain(ToDoItem item) => new ToDoItemCreateRequestDto
   (
        Name: item.Name,
        Description: item.Description,
        IsCompleted: item.IsCompleted
   );
}
