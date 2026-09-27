using System.Net.Http.Headers;
using System.Text;

namespace MauiBasicAuth.DataAccess
{
    public class UserAuthentication
    {
        public bool AuthenticateUser(string username, string password)
        {
            using var client = new HttpClient();

            // Match the HTTP port configured for the web service API.
            client.BaseAddress = new Uri("http://localhost:5049/");

            // Encode the credentials and send them using Basic authentication.
            var authToken = Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{username}:{password}"));
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", authToken);

            // Call the protected Values controller endpoint.
            using var response = client.GetAsync("api/Values").GetAwaiter().GetResult();

            // A successful response means the credentials were accepted.
            return response.IsSuccessStatusCode;
        }
    }
}
