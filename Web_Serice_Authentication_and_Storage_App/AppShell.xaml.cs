using Web_Serice_Authentication_and_Storage_App.Views;

namespace Web_Serice_Authentication_and_Storage_App;

public partial class AppShell : Shell
{
    public AppShell(MainPage loginPage)
    {
        InitializeComponent();
        Items.Add(new ShellContent { Title = "Login", Route = "Login", Content = loginPage });
        Routing.RegisterRoute(nameof(DataEntryPage), typeof(DataEntryPage));
    }
}
