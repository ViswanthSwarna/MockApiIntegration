using MockApiIntegration.Domain.Models;
using MockApiIntegration.Infrastructure.Clients.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace MockApiIntegration.Infrastructure.Clients.Classes
{
    // HTTP client implementation for interacting with the mock API.
    public class MockApiClient : IMockApiClient
    {
        private readonly HttpClient _httpClient;

        public MockApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.restful-api.dev");
        }

        public async Task<string> CreateProductAsync(MockApiProduct product)
        {
            try 
            { 
                var response = await _httpClient.PostAsJsonAsync("/objects", product);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadFromJsonAsync<MockApiProduct>();
                return result.Id;
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to communicate with external API", ex);
            }
        }

        public async Task<List<MockApiProduct>> GetProductsByIdsAsync(IEnumerable<string> ids)
        {
            try
            { 
                var idList = string.Join("&id=", ids);
                var response = await _httpClient.GetAsync($"/objects?id={idList}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();

                if (idList.Count() == 1)
                {
                    var singleProduct = JsonSerializer.Deserialize<MockApiProduct>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return new List<MockApiProduct> { singleProduct };
                }
                else
                {
                    var productList = JsonSerializer.Deserialize<List<MockApiProduct>>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return productList;
                }
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to communicate with external API", ex);
            }
        }

        public async Task DeleteProductAsync(string id)
        {
            try 
            {
                var response = await _httpClient.DeleteAsync($"/objects/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to communicate with external API", ex);
            }
        }
    }
}
