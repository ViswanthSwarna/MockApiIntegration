using System.Text.Json;

namespace MockApiIntegration.Application.DTOs
{
    // DTO used to return product data to clients.
    public class ProductResponseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public JsonElement Data { get; set; }
    }
}
