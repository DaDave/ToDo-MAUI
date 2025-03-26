using CommunityToolkit.Maui.Alerts;
using FRONTEND.ViewModels;
using Newtonsoft.Json;

namespace FRONTEND;

public partial class MainPage
{
    private readonly HttpClient _httpClient;
    private List<ToDoItem> _toDoItems;
    public MainPage()
    {
        _httpClient = new HttpClient();
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        //Request to Backend GETALL
        var result = await _httpClient.GetAsync(BackendConstants.TodoUrl);
        if (result.IsSuccessStatusCode)
        {
            var responseContent = await result.Content.ReadAsStringAsync();
            var resultingToDoItems = JsonConvert.DeserializeObject<List<ToDoItem>>(responseContent);
            _toDoItems = resultingToDoItems;
            ToDoListView.ItemsSource = resultingToDoItems;
        }
        else
        {
            await Toast.Make("Fehler beim Laden der ToDos: " + result.StatusCode)!.Show();
            ToDoListView.ItemsSource = null;
        }
    }

    private async void OnItemAdded(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ToDoItemPage(_httpClient)
        {
            BindingContext = new ToDoItem
            {
                IsCompleted = true
            }
        });
    }

    private async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            await Navigation.PushAsync(new ToDoItemPage(_httpClient)
            {
                BindingContext = e.SelectedItem as ToDoItem
            });
        }
    }

    private void OnFilterItemChanged(object? sender, EventArgs e)
    {
        var picker = (Picker)sender!;
        var selectedIndex = picker.SelectedIndex;
        
        ToDoListView.ItemsSource = selectedIndex switch
        {
            0 => ToDoListView.ItemsSource = _toDoItems,
            1 => ToDoListView.ItemsSource = _toDoItems.Where(x => x.IsCompleted == false).ToList(),
            _ => ToDoListView.ItemsSource = _toDoItems.Where(x => x.IsCompleted).ToList(),
        };
    }
}