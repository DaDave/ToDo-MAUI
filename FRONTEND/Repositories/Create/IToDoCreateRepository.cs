using FRONTEND.Models;

namespace FRONTEND.Repositories.Create;

public interface IToDoCreateRepository
{
    Task<bool> Create(ToDoItem toDoItem);
}