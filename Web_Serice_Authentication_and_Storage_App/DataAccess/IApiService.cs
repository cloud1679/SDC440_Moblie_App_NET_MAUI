using Web_Serice_Authentication_and_Storage_App.Models;

namespace Web_Serice_Authentication_and_Storage_App.DataAccess;

public interface IApiService
{
    Task<bool> LoginAsync(string userName, string password);
    Task<List<StoredItem>> GetItemsAsync();
    Task SaveItemAsync(StoredItem item);
}
