using FRONTEND.Models;
using FRONTEND.Repositories.Create;
using FRONTEND.Repositories.Delete;
using FRONTEND.Repositories.Update;
using FRONTEND.ViewModels;

namespace FRONTEND.Views;

[QueryProperty(nameof(ToDoItem), "Item")]
public partial class ToDoItemPage
{
    public ToDoItem ToDoItem
    {
        set => Load(value);
    }

    private readonly IToDoCreateRepository _toDoCreateRepository;
    private readonly IToDoUpdateRepository _toDoUpdateRepository;
    private readonly IToDoDeleteRepository _toDoDeleteRepository;

    public ToDoItemPage(
        IToDoCreateRepository toDoCreateRepository,
        IToDoUpdateRepository toDoUpdateRepository,
        IToDoDeleteRepository toDoDeleteRepository)
    {
        _toDoCreateRepository = toDoCreateRepository;
        _toDoUpdateRepository = toDoUpdateRepository;
        _toDoDeleteRepository = toDoDeleteRepository;
        InitializeComponent();
    }

    private void Load(ToDoItem toDoItem)
    {
        if (toDoItem != null)
        {
            BindingContext = new ToDoItemPageViewModel(
                toDoItem, 
                _toDoCreateRepository, 
                _toDoUpdateRepository,
                _toDoDeleteRepository);
        }
    }
}