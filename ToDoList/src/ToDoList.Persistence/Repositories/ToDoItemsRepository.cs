namespace ToDoList.Persistence.Repositories;
using ToDoList.Domain.Models;

public class ToDoItemsRepository : IRepository<ToDoItem>
{
    private readonly ToDoItemsContext context;

    public ToDoItemsRepository(ToDoItemsContext context)
    {
        this.context = context;
    }

    public void Create(ToDoItem item)
    {
        context.ToDoItems.Add(item);
        context.SaveChanges();
    }

    public List<ToDoItem> Read() => context.ToDoItems.ToList();

    public ToDoItem ReadById(int id) => context.ToDoItems.Find(id);

    public void DeleteById(int id)
    {
        var itemToDelete = context.ToDoItems.Find(id);

        if (itemToDelete != null)
        {
            context.ToDoItems.Remove(itemToDelete);
            context.SaveChanges();
        }
    }

    public void UpdateById(int id, ToDoItem updatedItem)
    {
        var itemToUpdate = context.ToDoItems.Find(id);

        if (itemToUpdate != null)
        {
            itemToUpdate.Name = updatedItem.Name;
            itemToUpdate.Description = updatedItem.Description;
            itemToUpdate.IsCompleted = updatedItem.IsCompleted;
            context.SaveChanges();
        }
    }
}
