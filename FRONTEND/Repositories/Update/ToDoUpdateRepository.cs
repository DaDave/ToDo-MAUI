using FRONTEND.Models;
using Newtonsoft.Json;

namespace FRONTEND.Repositories.Update;

public class ToDoUpdateRepository : IToDoUpdateRepository
{
    private readonly HttpClient _httpClient;

    public ToDoUpdateRepository()
    {
        _httpClient = new HttpClient();
    }

    public async Task<bool> Update(ToDoItem toDoItem)
    {
        var toDoItemJson = JsonConvert.SerializeObject(toDoItem);
        var httpContent = new StringContent(toDoItemJson, System.Text.Encoding.UTF8, "application/json");
        var result = await _httpClient.PutAsync(BackendConstants.TodoUpdateUrl + "/" + toDoItem.Id, httpContent);
        return result.IsSuccessStatusCode;
    }
}