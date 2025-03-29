namespace FRONTEND.Views;

public partial class AppShell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(ToDoItemPage), typeof(ToDoItemPage));
    }
}