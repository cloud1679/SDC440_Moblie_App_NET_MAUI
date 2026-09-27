using Web_Serice_Authentication_and_Storage_App.Views;

namespace Web_Serice_Authentication_and_Storage_App.Services;

public interface INavigationService
{
    Task ShowDataEntryAsync();
}

public sealed class NavigationService : INavigationService
{
    public Task ShowDataEntryAsync() => Shell.Current.GoToAsync(nameof(DataEntryPage));
}
