using CommunityToolkit.Maui.Alerts;
using FRONTEND.ViewModels;
using Newtonsoft.Json;

namespace FRONTEND;

public partial class ToDoItemPage
{
    private readonly HttpClient _httpClient;
    public ToDoItemPage(HttpClient httpClient)
    {
        _httpClient = httpClient;
        InitializeComponent();
    }

    private async void OnSaveClicked(object? sender, EventArgs e)
    {
        //Request to Backend CREATE or UPDATE
        var toDoItem = (ToDoItem)BindingContext;
        var toDoItemJson = JsonConvert.SerializeObject(toDoItem);
        var httpContent = new StringContent(toDoItemJson, System.Text.Encoding.UTF8, "application/json");
        HttpResponseMessage result;
        if (toDoItem.Id == default)
        {
            result = await _httpClient.PostAsync(BackendConstants.TodoUrl, httpContent);
        }
        else
        {
            result = await _httpClient.PutAsync(BackendConstants.TodoUrl + "/" + toDoItem.Id, httpContent);
        }
        if (!result.IsSuccessStatusCode)
        {
            await Toast.Make("Fehler beim Speichern des ToDos: " + result.StatusCode)!.Show();
        }
        await Navigation.PopAsync();
    }

    private async void OnDeleteClicked(object? sender, EventArgs e)
    {
        //Request to Backend DELETE
        var toDoItem = (ToDoItem)BindingContext;
        if (toDoItem.Id == default)
        {
            await Toast.Make("Fehler beim Löschen des ToDos: Noch nicht erstellte ToDos können nicht gelöscht werden.")!.Show();
            return;
        }
        var result = await _httpClient.DeleteAsync(BackendConstants.TodoUrl + "/" + toDoItem.Id);
        if (!result.IsSuccessStatusCode)
        {
            await Toast.Make("Fehler beim Löschen des ToDos: " + result.StatusCode)!.Show();
        }
        await Navigation.PopAsync();
    }

    private async void OnCancelClicked(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}