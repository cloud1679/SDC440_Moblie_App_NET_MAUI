using System.Collections.ObjectModel;
using Store_and_Retrieve_Date_SQLite.DataAccess;
using Store_and_Retrieve_Date_SQLite.Models;

namespace Store_and_Retrieve_Date_SQLite
{
	public partial class MainPage : ContentPage
	{
		private readonly ItemData itemData;

		public ObservableCollection<Item> Items { get; set; } = new();

		public MainPage()
		{
			InitializeComponent();

			itemData = new ItemData();

			BindingContext = this;

			UpdateItemList();
		}

		private async void OnSaveClicked(object? sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txtItemID.Text) ||
				string.IsNullOrWhiteSpace(txtItemName.Text) ||
				string.IsNullOrWhiteSpace(txtItemDescription.Text))
			{
				await DisplayAlertAsync(
					"Missing Information",
					"Please enter information in all fields.",
					"OK");
				return;
			}

			if (!int.TryParse(txtItemID.Text, out int itemID))
			{
				await DisplayAlertAsync(
					"Invalid Item ID",
					"Item ID must be a number.",
					"OK");
				return;
			}

			var item = new Item
			{
				ItemID = itemID,
				ItemName = txtItemName.Text,
				ItemDescription = txtItemDescription.Text
			};

			await itemData.SaveItemAsync(item);

			txtItemID.Text = string.Empty;
			txtItemName.Text = string.Empty;
			txtItemDescription.Text = string.Empty;

			UpdateItemList();
		}

		private async void UpdateItemList()
		{
			var items = await itemData.GetItemsAsync();

			Items.Clear();

			foreach (var item in items)
			{
				Items.Add(item);
			}
		}
	}
}