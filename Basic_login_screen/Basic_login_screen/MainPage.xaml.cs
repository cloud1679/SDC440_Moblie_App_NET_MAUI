namespace Basic_login_screen
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		private void OnLoginClicked(object? sender, EventArgs e)
		{
			string userId = txtUserName.Text ?? string.Empty;
			string password = txtPassword.Text ?? string.Empty;

			if (userId == "Burns" && password == "Password1")
			{
				lblMessage.Text = $"Login successful {userId}";
			}
			else
			{
				lblMessage.Text = $"Login failed {userId}";
			}
		}

		private void OnCancelClicked(object? sender, EventArgs e)
		{
			txtUserName.Text = string.Empty;
			txtPassword.Text = string.Empty;
			lblMessage.Text = string.Empty;
		}
	}
}