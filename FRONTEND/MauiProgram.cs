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
        builder.Services.AddScoped<IToDoCreateRepository, ToDoCreateRepository>();
        builder.Services.AddScoped<IToDoReadRepository, ToDoReadRepository>();
        builder.Services.AddScoped<IToDoUpdateRepository, ToDoUpdateRepository>();
        builder.Services.AddScoped<IToDoDeleteRepository, ToDoDeleteRepository>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<MainPageViewModel>();
        builder.Services.AddTransient<ToDoItemPage>();
        builder.Services.AddTransient<ToDoItemPageViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}