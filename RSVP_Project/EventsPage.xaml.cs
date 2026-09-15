namespace RSVP_Project;

public partial class EventsPage : ContentPage
{
    private readonly bool isLoggedIn;

    public EventsPage(bool loggedIn)
    {
        InitializeComponent();

        isLoggedIn = loggedIn;

        if (isLoggedIn)
        {
            lblUserStatus.Text = "Logged in as Burns";
            btnAttending.IsEnabled = true;
            btnHosting.IsEnabled = true;
            btnAddEvent.IsEnabled = true;
        }
        else
        {
            lblUserStatus.Text = "Viewing as Guest";
            btnAttending.IsEnabled = false;
            btnHosting.IsEnabled = false;
            btnAddEvent.IsEnabled = false;
        }

        ShowAllEvents();
    }

    private void ShowAllEvents()
    {
        lblListTitle.Text = "All Events";
        eventList.Children.Clear();

        AddEventButton("Birthday Party");
        AddEventButton("Family Cookout");
        AddEventButton("Graduation Party");
    }

    private void OnAllEventsClicked(object? sender, EventArgs e)
    {
        ShowAllEvents();
    }

    private void OnAttendingClicked(object? sender, EventArgs e)
    {
        lblListTitle.Text = "Events I Am Attending";
        eventList.Children.Clear();

        AddEventButton("Family Cookout");
    }

    private void OnHostingClicked(object? sender, EventArgs e)
    {
        lblListTitle.Text = "Events I Am Hosting";
        eventList.Children.Clear();

        AddEventButton("Birthday Party");
    }

    private void AddEventButton(string eventName)
    {
        Button eventButton = new Button
        {
            Text = eventName
        };

        eventButton.Clicked += async (sender, e) =>
        {
            await Navigation.PushAsync(
                new EventDetailsPage(eventName, isLoggedIn));
        };

        eventList.Children.Add(eventButton);
    }

    private async void OnAddEventClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddEventPage());
    }

    private async void OnLogoutClicked(object? sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }
}