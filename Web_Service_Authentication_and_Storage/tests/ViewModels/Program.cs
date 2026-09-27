using System.Windows.Input;
using Web_Serice_Authentication_and_Storage_App.DataAccess;
using Web_Serice_Authentication_and_Storage_App.Models;
using Web_Serice_Authentication_and_Storage_App.Services;
using Web_Serice_Authentication_and_Storage_App.ViewModels;

static void Check(bool value, string message) { if (!value) throw new Exception(message); }
var api = new FakeApi();
var nav = new FakeNavigation();
var login = new LoginViewModel(api, nav);
await login.LoginAsync();
Check(api.LoginCalls == 0 && login.Message.Length > 0, "Missing credentials");
login.UserName = "bad"; login.Password = "bad";
await login.LoginAsync();
Check(nav.Count == 0 && login.Message.Contains("failed"), "Failed login navigation");
login.CancelCommand.Execute(null);
Check(login.UserName == "" && login.Password == "" && login.Message == "", "Cancel clearing");
api.Authenticated = true; login.UserName = "Burns01"; login.Password = "Password1";
await login.LoginAsync();
Check(nav.Count == 1 && login.Password == "" && !login.IsBusy, "Successful login");
api.Offline = true;
login.Password = "Password1";
await login.LoginAsync();
Check(login.Message.Contains("connect") && !login.IsBusy, "Offline login");
api.Offline = false;
var data = new DataEntryViewModel(api);
await data.SaveAsync();
Check(api.SaveCalls == 0 && data.Message.Contains("Enter"), "Required item fields");
data.ItemId = " A1 "; data.ItemName = " Book "; data.ItemDescription = " Blue ";
await data.SaveAsync();
Check(data.Items.Count == 1 && data.Items[0].ItemId == "A1" && data.ItemId == "", "Save and retrieve");
data.ItemId = "A1"; data.ItemName = "Book"; data.ItemDescription = "Blue";
await data.SaveAsync();
Check(data.Message.Contains("exists") && data.ItemId == "A1" && !data.IsBusy, "Duplicate preserves input");
api.FailRead = true; data.ItemId = "A2";
await data.SaveAsync();
Check(data.Message.Contains("Item saved, but") && data.ItemId == "" && !data.IsBusy, "Saved but refresh failed");
api.FailRead = false;
await data.LoadAsync();
Check(data.Items.Count == 2, "Refresh recovery");
Console.WriteLine("PASS: login, cancel, navigation, offline handling, validation, save/reload, duplicate, refresh recovery");

// Minimal command adapter: tests exercise real view-model methods without a platform UI runtime.
public sealed class Command(Action action) : ICommand
{
    public bool CanExecute(object? parameter) => true;
    public void Execute(object? parameter) => action();
    public event EventHandler? CanExecuteChanged { add { } remove { } }
}
public sealed class FakeApi : IApiService
{
    public int LoginCalls, SaveCalls;
    public bool Authenticated, Offline, FailRead;
    private readonly List<StoredItem> items = [];
    public Task<bool> LoginAsync(string name, string password)
    {
        LoginCalls++;
        if (Offline) throw new HttpRequestException();
        return Task.FromResult(Authenticated);
    }
    public Task<List<StoredItem>> GetItemsAsync() => FailRead
        ? throw new HttpRequestException() : Task.FromResult(items.ToList());
    public Task SaveItemAsync(StoredItem item)
    {
        SaveCalls++;
        if (items.Any(i => i.ItemId == item.ItemId)) throw new InvalidOperationException("ID exists");
        items.Add(item);
        return Task.CompletedTask;
    }
}
public sealed class FakeNavigation : INavigationService
{
    public int Count;
    public Task ShowDataEntryAsync() { Count++; return Task.CompletedTask; }
}
namespace Web_Serice_Authentication_and_Storage_App.Services
{
    public interface INavigationService { Task ShowDataEntryAsync(); }
}
