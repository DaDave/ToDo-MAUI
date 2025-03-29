using FRONTEND.Models;

namespace FRONTEND.Repositories.Update;

public interface IToDoUpdateRepository
{
    Task<bool> Update(ToDoItem toDoItem);
}