using CommunityToolkit.Maui.Alerts;
using System.Windows.Input;
using FRONTEND.Models;
using FRONTEND.Repositories.Create;
using FRONTEND.Repositories.Delete;
using FRONTEND.Repositories.Update;

namespace FRONTEND.ViewModels;

public class ToDoItemPageViewModel
{
    private readonly IToDoCreateRepository _toDoCreateRepository;
    private readonly IToDoUpdateRepository _toDoUpdateRepository;
    private readonly IToDoDeleteRepository _toDoDeleteRepository;

    public ToDoItem? ToDoItem { get; set; }

    public ICommand SaveCommand { get; set; }
    public ICommand DeleteCommand { get; set; }
    public ICommand CancelCommand { get; set; }

    public ToDoItemPageViewModel(
        ToDoItem toDoItem, 
        IToDoCreateRepository toDoCreateRepository,
        IToDoUpdateRepository toDoUpdateRepository, 
        IToDoDeleteRepository toDoDeleteRepository)
    {
        ToDoItem = toDoItem;
        _toDoCreateRepository = toDoCreateRepository;
        _toDoUpdateRepository = toDoUpdateRepository;
        _toDoDeleteRepository = toDoDeleteRepository;

        SaveCommand = new Command(async () =>
        {
            bool isSuccessful;
            if (toDoItem.Id == default)
            {
                isSuccessful = await _toDoCreateRepository.Create(toDoItem);
            }
            else
            {
                isSuccessful = await _toDoUpdateRepository.Update(toDoItem);
            }

            if (!isSuccessful)
            {
                await Toast.Make("Fehler beim Speichern des ToDos").Show();
            }

            await Shell.Current.GoToAsync("..");
        });

        DeleteCommand = new Command(async () =>
        {
            if (toDoItem.Id == default)
            {
                await Toast.Make("Fehler beim Löschen des ToDos: Noch nicht erstellte ToDos können nicht gelöscht werden.")!.Show();
                return;
            }
            var isDeleted = await _toDoDeleteRepository.Delete(toDoItem.Id);
            if (!isDeleted)
            {
                await Toast.Make("Fehler beim Löschen des ToDos")!.Show();
            }
            await Shell.Current.GoToAsync("..");
        });

        CancelCommand = new Command(async () =>
        {
            await Shell.Current.GoToAsync("..");
        });
    }
}