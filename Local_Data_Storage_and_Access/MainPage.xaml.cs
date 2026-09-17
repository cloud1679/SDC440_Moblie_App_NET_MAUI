using System.Collections.ObjectModel;
using Local_Data_Storage_and_Access.DataAccess;
using Local_Data_Storage_and_Access.Models;

namespace Local_Data_Storage_and_Access
{
	public partial class MainPage : ContentPage
	{
		PersonData personData;

		public ObservableCollection<Person> People { get; set; } = new();

		public MainPage()
		{
			InitializeComponent();

			personData = new PersonData();

			BindingContext = this;

			UpdatePeopleList();
		}

		private async void OnSaveClicked(object? sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txtFirstName.Text))
			{
				await DisplayAlertAsync(
					"Error",
					"First name cannot be empty",
					"OK");
				return;
			}

			if (string.IsNullOrWhiteSpace(txtLastName.Text))
			{
				await DisplayAlertAsync(
					"Error",
					"Last name cannot be empty",
					"OK");
				return;
			}

			if (dpDateOfBirth.Date.HasValue &&
				dpDateOfBirth.Date.Value > DateTime.Today)
			{
				await DisplayAlertAsync(
					"Error",
					"Date of Birth cannot be in the future",
					"OK");
				return;
			}

			var person = new Person
			{
				FirstName = txtFirstName.Text,
				LastName = txtLastName.Text,
				DoB = dpDateOfBirth.Date ?? DateTime.Today
			};

			await personData.SavePersonAsync(person);

			UpdatePeopleList();
		}

		private async void UpdatePeopleList()
		{
			var people = await personData.GetPeopleAsync();

			People.Clear();

			foreach (var person in people)
			{
				People.Add(person);
			}
		}
	}
}