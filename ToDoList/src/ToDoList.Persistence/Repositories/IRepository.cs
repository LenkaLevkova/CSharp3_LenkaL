namespace ToDoList.Persistence.Repositories;

using System.Runtime.InteropServices;
using ToDoList.Domain.Models;

public interface IRepository<T> where T : class
{
    void Create(T item);
    List<T> Read();
    T ReadById(int id);
    void DeleteById(int id);
    void UpdateById(int id, T updatedItem);
}
