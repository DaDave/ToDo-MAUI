using FRONTEND.Models;
using Newtonsoft.Json;

namespace FRONTEND.Repositories.Read;

public class ToDoReadRepository : IToDoReadRepository
{
    private readonly HttpClient _httpClient;

    public ToDoReadRepository()
    {
        _httpClient = new HttpClient();
    }

    public async Task<List<ToDoItem>?> Read()
    {
        var result = await _httpClient.GetAsync(BackendConstants.TodoUrl);
        if (!result.IsSuccessStatusCode) return null;
        var responseContent = await result.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<ToDoItem>>(responseContent);

    }
}