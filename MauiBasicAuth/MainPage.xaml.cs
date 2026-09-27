namespace MauiBasicAuth;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object? sender, EventArgs e)
    {
        // Get the username and password from the entry fields.
        string username = txtUserId.Text ?? string.Empty;
        string password = txtPassword.Text ?? string.Empty;

        var loginButton = sender as Button;
        if (loginButton is not null)
            loginButton.IsEnabled = false;

        try
        {
            var userAuth = new DataAccess.UserAuthentication();

            // Keep the UI responsive while the synchronous API call runs.
            bool isAuthenticated = await Task.Run(
                () => userAuth.AuthenticateUser(username, password));

            if (isAuthenticated)
            {
                await DisplayAlertAsync("Success", "User authenticated successfully!", "OK");
            }
            else
            {
                await DisplayAlertAsync("Error", "Invalid username or password.", "OK");
            }
        }
        catch (HttpRequestException)
        {
            await DisplayAlertAsync("Error", "Unable to connect to the authentication service. Make sure the Web API is running.", "OK");
        }
        catch (TaskCanceledException)
        {
            await DisplayAlertAsync("Error", "The authentication request timed out. Please try again.", "OK");
        }
        finally
        {
            if (loginButton is not null)
                loginButton.IsEnabled = true;
        }
    }
}
