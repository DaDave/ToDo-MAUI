using FRONTEND.Models;

namespace FRONTEND.Repositories.Read;

public interface IToDoReadRepository
{
    Task<List<ToDoItem>?> Read();
}