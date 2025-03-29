using CommunityToolkit.Maui.Alerts;
using System.Windows.Input;
using FRONTEND.Models;
using FRONTEND.Repositories.Read;
using FRONTEND.Views;

namespace FRONTEND.ViewModels;

public class MainPageViewModel
{
    private readonly IToDoReadRepository _toDoReadRepository;
    
    public List<ToDoItem> ToDoItems { get; set; }
    private List<ToDoItem> OriginToDoItems { get; set; }

    public ICommand AppearingCommand { get; set; }
    public ICommand AddItemCommand { get; set; }
    public ICommand ItemSelectedCommand { get; set; }

    public MainPageViewModel(IToDoReadRepository toDoReadRepository)
    {
        ToDoItems = [];
        _toDoReadRepository = toDoReadRepository;

        AppearingCommand = new Command(async () =>
        {
            var resultingToDoItems = await _toDoReadRepository.Read();
            if (resultingToDoItems != null)
            {
                ToDoItems = resultingToDoItems;
                OriginToDoItems = resultingToDoItems;
            }
            else
            {
                await Toast.Make("Fehler beim Laden der ToDos")!.Show();
                ToDoItems = [];
                OriginToDoItems = [];
            }
        });

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