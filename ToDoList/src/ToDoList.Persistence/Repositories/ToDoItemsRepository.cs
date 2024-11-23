namespace ToDoList.Persistence.Repositories;

using System.Collections.Generic;
using ToDoList.Domain.Models;

public class ToDoItemsRepository : IRepositoryAsync<ToDoItem>
{
    private readonly ToDoItemsContext context;

    public ToDoItemsRepository(ToDoItemsContext context)
    {
        this.context = context;
    }

    public async Task CreateAsync(ToDoItem item)
    {
        await context.ToDoItems.AddAsync(item);
        await context.SaveChangesAsync();
    }

    //how can I await .ToList() ??
    public async Task<IEnumerable<ToDoItem>> ReadAllAsync() => context.ToDoItems.ToList();
    public async Task<ToDoItem?> ReadByIdAsync(int id) => await context.ToDoItems.FindAsync(id);
    public async Task UpdateAsync(ToDoItem item)
    {
        //This did not work for me, so I am replacing it with more stupid, but functional solution
        //context.Entry(item).CurrentValues.SetValues(item);
        //context.SaveChanges();

        var itemToUpdate = await context.ToDoItems.FindAsync(item.ToDoItemId);

        if (itemToUpdate != null)
        {
            itemToUpdate.Name = item.Name;
            itemToUpdate.Description = item.Description;
            itemToUpdate.IsCompleted = item.IsCompleted;
            itemToUpdate.Category = item.Category;
            await context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(ToDoItem item)
    {
        context.ToDoItems.Remove(item);
        await context.SaveChangesAsync();
    }
}
