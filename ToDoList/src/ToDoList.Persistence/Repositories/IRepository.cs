namespace ToDoList.Persistence.Repositories;

using System.Runtime.InteropServices;
using ToDoList.Domain.Models;

public interface IRepository<T> where T : class
{
    void Create(T item);

}
