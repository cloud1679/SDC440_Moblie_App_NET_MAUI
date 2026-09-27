using System.Windows.Input;
using Web_Serice_Authentication_and_Storage_App.DataAccess;
using Web_Serice_Authentication_and_Storage_App.Services;

namespace Web_Serice_Authentication_and_Storage_App.ViewModels;

public sealed class LoginViewModel : BaseViewModel
{
    private readonly IApiService api;
    private readonly INavigationService navigation;
    private string userName = "";
    private string password = "";
    public string UserName { get => userName; set => SetProperty(ref userName, value); }
    public string Password { get => password; set => SetProperty(ref password, value); }
    public ICommand LoginCommand { get; }
    public ICommand CancelCommand { get; }

    public LoginViewModel(IApiService api, INavigationService navigation)
    {
        this.api = api;
        this.navigation = navigation;
        LoginCommand = new Command(async () => await LoginAsync());
        CancelCommand = new Command(Cancel);
    }

    public void Cancel()
    {
        if (IsBusy) return;
        UserName = Password = Message = "";
    }

    public async Task LoginAsync()
    {
        if (IsBusy) return;
        Message = "";
        if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrEmpty(Password))
        {
            Message = "Enter your user name and password.";
            return;
        }
        IsBusy = true;
        try
        {
            if (!await api.LoginAsync(UserName, Password))
            {
                Message = "Login failed. Check your user name and password.";
                return;
            }
            Password = "";
            await navigation.ShowDataEntryAsync();
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException)
        {
            Message = "Unable to connect to the web service. Check the connection and try again.";
        }
        finally { IsBusy = false; }
    }
}
