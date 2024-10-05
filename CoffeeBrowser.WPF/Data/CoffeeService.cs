using CoffeeBrowser.Library.Data;
using System.Net.Http;
using System.Net.Http.Json;

namespace CoffeeBrowser.WPF.Data;

public class CoffeeService : ICoffeeService
{
    private readonly HttpClient _httpClient = new(); // load data from URL

    public async Task<IEnumerable<Coffee>?> LoadCoffeesAsync()
    {
        var coffees = await _httpClient.GetFromJsonAsync<IEnumerable<Coffee>>(
            "https://thomasclaudiushuber.com/pluralsight/coffees.json");

        return coffees;
    }
}
