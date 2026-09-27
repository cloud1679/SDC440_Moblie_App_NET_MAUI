using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Web_Serice_Authentication_and_Storage_App.Models;

namespace Web_Serice_Authentication_and_Storage_App.DataAccess;

public sealed class ApiService(HttpClient client) : IApiService
{
    public async Task<bool> LoginAsync(string userName, string password)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "api/auth/login");
        request.Headers.Authorization = new AuthenticationHeaderValue("Basic",
            Convert.ToBase64String(Encoding.UTF8.GetBytes($"{userName}:{password}")));
        using var response = await client.SendAsync(request);
        if (response.StatusCode == HttpStatusCode.Unauthorized) return false;
        response.EnsureSuccessStatusCode();
        return true;
    }

    public async Task<List<StoredItem>> GetItemsAsync() =>
        await client.GetFromJsonAsync<List<StoredItem>>("api/items") ?? [];

    public async Task SaveItemAsync(StoredItem item)
    {
        using var response = await client.PostAsJsonAsync("api/items", item);
        if (response.StatusCode == HttpStatusCode.Conflict)
            throw new InvalidOperationException("This Item ID already exists. Enter a different ID.");
        response.EnsureSuccessStatusCode();
    }
}
