using CommunityToolkit.Maui.Alerts;
using System.Windows.Input;
using FRONTEND.Models;
using FRONTEND.Repositories.Create;
using FRONTEND.Repositories.Delete;
using FRONTEND.Repositories.Update;

namespace FRONTEND.ViewModels;

public class ToDoItemPageViewModel
{
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

        SaveCommand = new Command(async () =>
        {
            bool isSuccessful;
            if (toDoItem.Id == default)
            {
                isSuccessful = await toDoCreateRepository.Create(toDoItem);
            }
            else
            {
                isSuccessful = await toDoUpdateRepository.Update(toDoItem);
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
            var isDeleted = await toDoDeleteRepository.Delete(toDoItem.Id);
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