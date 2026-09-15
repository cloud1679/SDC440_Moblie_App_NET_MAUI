namespace RSVP_Project;

public partial class RSVPPage : ContentPage
{
    public RSVPPage(string eventName, bool isLoggedIn)
    {
        InitializeComponent();

        lblEventName.Text = eventName;

        if (isLoggedIn)
        {
            // Hard-coded user information for this phase
            txtName.Text = "Richard Burns";
            txtEmail.Text = "richard@example.com";
            txtPhone.Text = "555-123-4567";
        }
        else
        {
            // Guest fields should start blank
            txtName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPhone.Text = string.Empty;
        }
    }

    private async void OnRSVPClicked(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtName.Text) ||
            string.IsNullOrWhiteSpace(txtEmail.Text) ||
            string.IsNullOrWhiteSpace(txtPhone.Text))
        {
            lblMessage.Text = "Please complete all fields.";
            return;
        }

        // Actual RSVP saving will be added later.
        lblMessage.Text = string.Empty;

        await Navigation.PopAsync();
    }

    private async void OnCancelClicked(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}