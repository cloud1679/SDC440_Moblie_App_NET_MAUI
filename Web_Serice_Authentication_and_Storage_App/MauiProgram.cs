using Microsoft.Extensions.Logging;
using Web_Serice_Authentication_and_Storage_App.DataAccess;
using Web_Serice_Authentication_and_Storage_App.Services;
using Web_Serice_Authentication_and_Storage_App.ViewModels;
using Web_Serice_Authentication_and_Storage_App.Views;

namespace Web_Serice_Authentication_and_Storage_App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        });
        // Android emulator reaches the host at 10.0.2.2; Apple simulators use localhost.
        // For a physical device, replace this with the server's reachable HTTPS URL.
        var baseUrl = DeviceInfo.Platform == DevicePlatform.Android
            ? "http://10.0.2.2:5027/" : "http://localhost:5027/";
        builder.Services.AddSingleton(new HttpClient
        {
            BaseAddress = new Uri(baseUrl), Timeout = TimeSpan.FromSeconds(15)
        });
        builder.Services.AddSingleton<IApiService, ApiService>();
        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<DataEntryViewModel>();
        builder.Services.AddTransient<DataEntryPage>();
        builder.Services.AddSingleton<AppShell>();
#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}
