using Shop.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Services;

public class CurrencyService : ICurrencyService
{
    private readonly HttpClient _httpClient;

    public CurrencyService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<decimal> GetUsdRateAsync()
    {
        var response = await _httpClient.GetAsync("pathToServer");

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(content);

        return (decimal)data.rates.UAH;
    }
}