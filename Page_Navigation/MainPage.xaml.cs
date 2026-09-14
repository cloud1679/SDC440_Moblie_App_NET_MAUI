namespace Page_Navigation
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		private async void OnSendMessageClicked(object? sender, EventArgs e)
		{
			await Navigation.PushAsync(new MessagePage(txtMessage.Text ?? string.Empty));
		}
	}
}