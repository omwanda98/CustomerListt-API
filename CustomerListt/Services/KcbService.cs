using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace CustomerListt.Services
{
    public class KcbService
    {
        private readonly HttpClient _httpClient;

        public KcbService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> FetchTokenAsync(string username, string password)
        {
            var url = "https://uat.buni.kcbgroup.com/token?grant_type=client_credentials";
            var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

            var response = await _httpClient.PostAsync(url, null);
            response.EnsureSuccessStatusCode(); // exception if not success

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return jsonResponse; // Returns the full JSON response, for bearer, expires, token
        }
    }
}
