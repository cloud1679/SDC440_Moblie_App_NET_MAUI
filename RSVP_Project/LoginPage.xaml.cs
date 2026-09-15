namespace RSVP_Project;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object? sender, EventArgs e)
    {
        // Hard-coded login for this phase of the project
        if (txtUserName.Text == "Burns" &&
            txtPassword.Text == "Password1")
        {
            lblMessage.Text = string.Empty;

            await Navigation.PushAsync(
                new EventsPage(true));
        }
        else
        {
            lblMessage.Text = "Invalid user name or password.";
        }
    }

    private async void OnGuestClicked(object? sender, EventArgs e)
    {
        lblMessage.Text = string.Empty;

        await Navigation.PushAsync(
            new EventsPage(false));
    }

    private async void OnCreateAccountClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(
            new AddUserPage());
    }
}