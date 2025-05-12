using System.Net.Http.Json;
using System.Text.Json;

namespace MockApiIntegration.Application.DTOs
{
    // DTO used for creating a product.
    public class ProductRequestDto
    {
        public string Name { get; set; }
        public JsonElement Data { get; set; }
    }
}
