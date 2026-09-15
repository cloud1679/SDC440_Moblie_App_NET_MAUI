namespace RSVP_Project;

public partial class EventDetailsPage : ContentPage
{
    private readonly string eventName;
    private readonly bool isLoggedIn;

    public EventDetailsPage(string selectedEvent, bool loggedIn)
    {
        InitializeComponent();

        eventName = selectedEvent;
        isLoggedIn = loggedIn;

        LoadEventDetails();
    }

    private void LoadEventDetails()
    {
        lblEventName.Text = eventName;

        if (eventName == "Birthday Party")
        {
            lblHostName.Text = "Richard Burns";
            lblLocation.Text = "Virginia Beach";
            lblDateTime.Text = "September 20, 2026 at 6:00 PM";
            lblMaximum.Text = "20";
            lblCurrent.Text = "8";
            lblDeadline.Text = "September 18, 2026";
            lblAttendees.Text = "Richard, John, Sarah, Mike";
        }
        else if (eventName == "Family Cookout")
        {
            lblHostName.Text = "John Smith";
            lblLocation.Text = "City Park";
            lblDateTime.Text = "September 26, 2026 at 2:00 PM";
            lblMaximum.Text = "30";
            lblCurrent.Text = "14";
            lblDeadline.Text = "September 24, 2026";
            lblAttendees.Text = "John, Richard, Susan, David";
        }
        else
        {
            lblHostName.Text = "Sarah Jones";
            lblLocation.Text = "Community Center";
            lblDateTime.Text = "October 3, 2026 at 5:00 PM";
            lblMaximum.Text = "50";
            lblCurrent.Text = "22";
            lblDeadline.Text = "October 1, 2026";
            lblAttendees.Text = "Sarah, Mike, Lisa, David";
        }
    }

    private async void OnRSVPClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(
            new RSVPPage(eventName, isLoggedIn));
    }

    private async void OnGoBackClicked(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}