using FRONTEND.Models;
using Newtonsoft.Json;

namespace FRONTEND.Repositories.Create;

public class ToDoCreateRepository : IToDoCreateRepository
{
    private readonly HttpClient _httpClient;

    public ToDoCreateRepository()
    {
        _httpClient = new HttpClient();
    }

    public async Task<bool> Create(ToDoItem toDoItem)
    {
        var toDoItemJson = JsonConvert.SerializeObject(toDoItem);
        var httpContent = new StringContent(toDoItemJson, System.Text.Encoding.UTF8, "application/json");
        var result = await _httpClient.PostAsync(BackendConstants.TodoUrl, httpContent);
        return result.IsSuccessStatusCode;
    }
}