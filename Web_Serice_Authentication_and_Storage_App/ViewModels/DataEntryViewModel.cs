using System.Collections.ObjectModel;
using System.Windows.Input;
using Web_Serice_Authentication_and_Storage_App.DataAccess;
using Web_Serice_Authentication_and_Storage_App.Models;

namespace Web_Serice_Authentication_and_Storage_App.ViewModels;

public sealed class DataEntryViewModel : BaseViewModel
{
    private readonly IApiService api;
    private string itemId = "", itemName = "", itemDescription = "";
    public string ItemId { get => itemId; set => SetProperty(ref itemId, value); }
    public string ItemName { get => itemName; set => SetProperty(ref itemName, value); }
    public string ItemDescription { get => itemDescription; set => SetProperty(ref itemDescription, value); }
    public ObservableCollection<StoredItem> Items { get; } = [];
    public ICommand SaveCommand { get; }
    public ICommand RefreshCommand { get; }

    public DataEntryViewModel(IApiService api)
    {
        this.api = api;
        SaveCommand = new Command(async () => await SaveAsync());
        RefreshCommand = new Command(async () => await LoadAsync());
    }

    private async Task RefreshItemsAsync()
    {
        var items = await api.GetItemsAsync();
        Items.Clear();
        foreach (var item in items) Items.Add(item);
    }

    public async Task LoadAsync()
    {
        if (IsBusy) return;
        IsBusy = true;
        Message = "";
        try { await RefreshItemsAsync(); }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException or System.Text.Json.JsonException)
        {
            Message = "Unable to load stored items. Check the connection and tap Refresh.";
        }
        finally { IsBusy = false; }
    }

    public async Task SaveAsync()
    {
        if (IsBusy) return;
        if (string.IsNullOrWhiteSpace(ItemId) || string.IsNullOrWhiteSpace(ItemName)
            || string.IsNullOrWhiteSpace(ItemDescription))
        {
            Message = "Enter Item ID, Item Name, and Item Description before saving.";
            return;
        }
        IsBusy = true;
        Message = "";
        var saved = false;
        try
        {
            await api.SaveItemAsync(new StoredItem
            {
                ItemId = ItemId.Trim(), ItemName = ItemName.Trim(),
                ItemDescription = ItemDescription.Trim()
            });
            saved = true;
            ItemId = ItemName = ItemDescription = "";
            await RefreshItemsAsync();
            Message = "Item saved successfully.";
        }
        catch (InvalidOperationException ex) { Message = ex.Message; }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException or System.Text.Json.JsonException)
        {
            Message = saved
                ? "Item saved, but the list could not be refreshed. Tap Refresh to reload it."
                : "Could not confirm the save. Check the connection and refresh the list before retrying.";
        }
        finally { IsBusy = false; }
    }
}
