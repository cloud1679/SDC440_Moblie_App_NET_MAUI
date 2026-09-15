namespace RSVP_Project;

public partial class AddEventPage : ContentPage
{
    public AddEventPage()
    {
        InitializeComponent();
    }

    private async void OnSaveClicked(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtHostName.Text) ||
            string.IsNullOrWhiteSpace(txtEventName.Text) ||
            string.IsNullOrWhiteSpace(txtAddress.Text) ||
            string.IsNullOrWhiteSpace(txtMaxAttendees.Text))
        {
            lblMessage.Text = "Please complete all fields.";
            return;
        }

        // Saving event data will be added later.
        lblMessage.Text = string.Empty;

        await Navigation.PopAsync();
    }

    private async void OnCancelClicked(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}