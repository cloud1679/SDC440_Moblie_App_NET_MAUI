using Local_Data_Storage_and_Access.Models;
using Newtonsoft.Json;
using System.Text;

namespace Local_Data_Storage_and_Access.DataAccess
{
    public class PersonData
    {
        private static readonly HttpClient client = new HttpClient();

        private const string ApiUrl =
            "http://localhost:5156/api/Person";

        public async Task<List<Person>> GetPeopleAsync()
        {
            using var response = await client.GetAsync(ApiUrl);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<Person>>(content)
                ?? new List<Person>();
        }

        public async Task<int> SavePersonAsync(Person person)
        {
            var json = JsonConvert.SerializeObject(person);

            using var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            using var response = await client.PostAsync(ApiUrl, content);

            return response.IsSuccessStatusCode ? 1 : 0;
        }
    }
}