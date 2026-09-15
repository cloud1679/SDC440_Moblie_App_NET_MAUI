namespace Passing_Data_Between_Pages
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		private async void OnAdditionClicked(object? sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txtFirstValue.Text) ||
				string.IsNullOrWhiteSpace(txtSecondValue.Text))
			{
				lblMessage.Text =
					"Please enter values for both fields to get the sum";
				return;
			}

			double firstValue = Convert.ToDouble(txtFirstValue.Text);
			double secondValue = Convert.ToDouble(txtSecondValue.Text);

			lblMessage.Text = string.Empty;

			await Navigation.PushAsync(
				new ResultPage(firstValue, secondValue, "Addition"));
		}

		private async void OnSubtractionClicked(object? sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txtFirstValue.Text) ||
				string.IsNullOrWhiteSpace(txtSecondValue.Text))
			{
				lblMessage.Text =
					"Please enter values for both fields to get the difference";
				return;
			}

			double firstValue = Convert.ToDouble(txtFirstValue.Text);
			double secondValue = Convert.ToDouble(txtSecondValue.Text);

			lblMessage.Text = string.Empty;

			await Navigation.PushAsync(
				new ResultPage(firstValue, secondValue, "Subtraction"));
		}
	}
}