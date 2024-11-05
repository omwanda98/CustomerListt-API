using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

public class CustomerService
{
    private readonly HttpClient _httpClient;

    public CustomerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Customer>> GetCustomersAsync()
    {
        var url = "http://desktop-evfbubg:7048/BC240/ODataV4/Company('CRONUS%20International%20Ltd.')/CustomerListData"; //BC ODATA LINK/SERVICE

        // Make the HTTP request using default credentials (for Windows Authentication)
        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var jsonData = JsonDocument.Parse(jsonString);

            // Retrieve the "value" property containing the array of customer data
            if (jsonData.RootElement.TryGetProperty("value", out JsonElement customerList))
            {
                var customers = new List<Customer>();

                foreach (var element in customerList.EnumerateArray())
                {
                    var customer = new Customer
                    {
                        No = element.GetProperty("No").GetString(),
                        Name = element.GetProperty("Name").GetString(),
                        Contact = element.GetProperty("Contact").GetString(),
                        Location_Code = element.GetProperty("Location_Code").GetString(),
                        Post_Code = element.GetProperty("Post_Code").GetString(),
                        Balance_LCY = element.GetProperty("Balance_LCY").GetDecimal()
                    };

                    customers.Add(customer);
                }

                return customers;
            }
        }

        return new List<Customer>();
    }
}

//MODEL Definition
public class Customer
{
    public required string No { get; set; }
    public required string Name { get; set; }
    public required string Contact { get; set; }
    public required string Location_Code { get; set; }
    public required string Post_Code { get; set; }
    public decimal Balance_LCY { get; set; }
}
