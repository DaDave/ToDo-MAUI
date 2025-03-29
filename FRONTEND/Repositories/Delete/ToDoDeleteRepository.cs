namespace FRONTEND.Repositories.Delete;

public class ToDoDeleteRepository : IToDoDeleteRepository
{
    private readonly HttpClient _httpClient;

    public ToDoDeleteRepository()
    {
        _httpClient = new HttpClient();
    }

    public async Task<bool> Delete(int id)
    {
        var result = await _httpClient.DeleteAsync(BackendConstants.TodoUrl + "/" + id);
        return result.IsSuccessStatusCode;
    }
}