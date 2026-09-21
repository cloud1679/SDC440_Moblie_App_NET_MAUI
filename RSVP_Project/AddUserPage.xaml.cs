namespace RSVP_Project;

public partial class AddUserPage : ContentPage
{
    private readonly AppDatabase database = new AppDatabase();

    public AddUserPage()
    {
        InitializeComponent();
    }

    private async void OnAddClicked(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtName.Text) ||
            string.IsNullOrWhiteSpace(txtUserName.Text) ||
            string.IsNullOrWhiteSpace(txtEmail.Text) ||
            string.IsNullOrWhiteSpace(txtPassword.Text) ||
            string.IsNullOrWhiteSpace(txtPhone.Text))
        {
            lblMessage.Text = "Please complete all fields.";
            return;
        }

        User newUser = new User
        {
            Name = txtName.Text,
            UserName = txtUserName.Text,
            Email = txtEmail.Text,
            Password = txtPassword.Text,
            PhoneNumber = txtPhone.Text
        };

        await database.AddUserAsync(newUser);

        await DisplayAlertAsync(
            "Account Created",
            "Your account was created successfully.",
            "OK");

        await Navigation.PopAsync();
    }

    private async void OnCancelClicked(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}