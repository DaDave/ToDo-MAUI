using CommunityToolkit.Maui.Alerts;
using FRONTEND.Models;
using FRONTEND.Repositories.Read;
using FRONTEND.ViewModels;

namespace FRONTEND.Views;

public partial class MainPage
{
    private readonly MainPageViewModel _mainPageViewModel;
    private readonly IToDoReadRepository _toDoReadRepository;
    public MainPage(IToDoReadRepository toDoReadRepository, MainPageViewModel mainPageViewModel)
    {
        _toDoReadRepository = toDoReadRepository;
        _mainPageViewModel = mainPageViewModel;
        InitializeComponent();
        BindingContext = mainPageViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var toDoItems = await _toDoReadRepository.Read();
        if (toDoItems != null)
        {
            _mainPageViewModel.ToDoItems = toDoItems;
            ToDoListView.ItemsSource = toDoItems;
        }
        else
        {
            await Toast.Make("Fehler beim Laden der ToDos:")!.Show();
            _mainPageViewModel.ToDoItems = [];
            ToDoListView.ItemsSource = null;
        }
        ToDoListView.ItemsSource = toDoItems;
    }

    private void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            _mainPageViewModel.ItemSelectedCommand.Execute(e.SelectedItem as ToDoItem);
        }
    }

    private void OnFilterItemChanged(object? sender, EventArgs e)
    {
        var picker = (Picker)sender!;
        var selectedIndex = picker.SelectedIndex;
        
        ToDoListView.ItemsSource = selectedIndex switch
        {
            0 => ToDoListView.ItemsSource = _mainPageViewModel.ToDoItems,
            1 => ToDoListView.ItemsSource = _mainPageViewModel.ToDoItems.Where(x => x.IsCompleted == false).ToList(),
            _ => ToDoListView.ItemsSource = _mainPageViewModel.ToDoItems.Where(x => x.IsCompleted).ToList(),
        };
    }
}