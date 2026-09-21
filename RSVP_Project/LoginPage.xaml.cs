namespace RSVP_Project;

public partial class LoginPage : ContentPage
{
    private readonly AppDatabase database = new AppDatabase();

    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtUserName.Text) ||
            string.IsNullOrWhiteSpace(txtPassword.Text))
        {
            lblMessage.Text = "Please enter your user name and password.";
            return;
        }

        User? user = await database.GetUserByLoginAsync(
            txtUserName.Text,
            txtPassword.Text);

        if (user != null)
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
        await Navigation.PushAsync(new AddUserPage());
    }
}