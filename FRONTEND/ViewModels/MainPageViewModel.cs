using CommunityToolkit.Maui.Alerts;
using System.Windows.Input;
using FRONTEND.Models;
using FRONTEND.Repositories.Read;
using FRONTEND.Views;

namespace FRONTEND.ViewModels;

public class MainPageViewModel
{
    public List<ToDoItem> ToDoItems { get; set; }

    public ICommand AddItemCommand { get; set; }
    public ICommand ItemSelectedCommand { get; set; }

    public MainPageViewModel()
    {
        ToDoItems = [];

        AddItemCommand = new Command(async () =>
        {
            await Shell.Current.GoToAsync(nameof(ToDoItemPage), new Dictionary<string, object>
            {
                {
                    "Item", new ToDoItem
                    {
                        IsCompleted = false
                    }
                }
            });
        });
        
        ItemSelectedCommand = new Command(async item =>
        {
            await Shell.Current.GoToAsync(nameof(ToDoItemPage),
                new Dictionary<string, object> { { "Item", (item as ToDoItem)! } });
        });
    }
}