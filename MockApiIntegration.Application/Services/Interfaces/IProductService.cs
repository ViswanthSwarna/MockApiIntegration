using MockApiIntegration.Application.DTOs;

namespace MockApiIntegration.Application.Services.Interfaces
{
    // Service interface defining core product operations.
    public interface IProductService
    {
        Task<string> AddProductAsync(ProductRequestDto dto);
        Task<List<ProductResponseDto>> GetProductsAsync(string nameFilter, int page, int pageSize);
        Task DeleteProductAsync(string id);
    }
}
