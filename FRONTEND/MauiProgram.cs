using CommunityToolkit.Maui;
using FRONTEND.Models;
using FRONTEND.Repositories.Create;
using FRONTEND.Repositories.Delete;
using FRONTEND.Repositories.Read;
using FRONTEND.Repositories.Update;
using FRONTEND.ViewModels;
using FRONTEND.Views;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

namespace FRONTEND;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        builder.Services.AddTransient<IToDoCreateRepository, ToDoCreateRepository>();
        builder.Services.AddTransient<IToDoReadRepository, ToDoReadRepository>();
        builder.Services.AddTransient<IToDoUpdateRepository, ToDoUpdateRepository>();
        builder.Services.AddTransient<IToDoDeleteRepository, ToDoDeleteRepository>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddTransient<ToDoItemPage>();
        builder.Services.AddTransient<ToDoItemPageViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}