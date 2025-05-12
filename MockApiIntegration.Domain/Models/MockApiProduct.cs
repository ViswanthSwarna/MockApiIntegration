using System.Text.Json;

namespace MockApiIntegration.Domain.Models
{
    // Represents the structure of a product as expected by the mock API.
    public class MockApiProduct
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public JsonElement Data { get; set; }
    }

}
