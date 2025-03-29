namespace FRONTEND.Repositories.Delete;

public interface IToDoDeleteRepository
{
    Task<bool> Delete(int id);
}