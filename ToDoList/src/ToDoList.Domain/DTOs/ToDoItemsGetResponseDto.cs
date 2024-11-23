namespace ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;

// public class ToDoItemGetResponseDto
// {
//     public int ToDoItemId { get; set; }
//     public string Name { get; set; }
//     public string Description { get; set; }
//     public bool IsCompleted { get; set; }

//     public static ToDoItemGetResponseDto FromDomain(ToDoItem item)
//     {
//         return new ToDoItemGetResponseDto
//         {
//             Name = item.Name,
//             Description = item.Description,
//             IsCompleted = item.IsCompleted,
//         };
//     }
// }

public record class ToDoItemGetResponseDto(int Id, string Name, string Description, bool IsCompleted, string? Category = null) //let client know the Id
{
    public static ToDoItemGetResponseDto FromDomain(ToDoItem item) => new(item.ToDoItemId, item.Name, item.Description, item.IsCompleted, item.Category);
}
