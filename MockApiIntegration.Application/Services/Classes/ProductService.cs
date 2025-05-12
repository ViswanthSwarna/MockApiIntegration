using MockApiIntegration.Application.DTOs;
using MockApiIntegration.Application.Exceptions;
using MockApiIntegration.Application.Services.Interfaces;
using MockApiIntegration.Domain.Models;
using MockApiIntegration.Infrastructure.Clients.Interfaces;
using MockApiIntegration.Infrastructure.Storage.Interfaces;

namespace MockApiIntegration.Application.Services.Classes
{
    // Service layer implementing product operations and coordinating storage + API calls.
    public class ProductService : IProductService
    {
        private readonly IProductIdStore _idStore;
        private readonly IMockApiClient _apiClient;

        public ProductService(IProductIdStore idStore, IMockApiClient apiClient)
        {
            _idStore = idStore;
            _apiClient = apiClient;
        }

        public async Task<string> AddProductAsync(ProductRequestDto dto)
        {
            var product = new MockApiProduct
            {
                Name = dto.Name,
                Data = dto.Data
            };

            var id = await _apiClient.CreateProductAsync(product);
            _idStore.Add(id, product.Name); // Track the created product ID and name
            return id;
        }

        public async Task<List<ProductResponseDto>> GetProductsAsync(string nameFilter, int page, int pageSize)
        {
            var pagedIds = _idStore.GetPagedIds(nameFilter, page, pageSize);
            if (!pagedIds.Any()) return new List<ProductResponseDto>();

            var products = await _apiClient.GetProductsByIdsAsync(pagedIds);

            return products.Select(p => new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Data = p.Data
            }).ToList();
        }

        public async Task DeleteProductAsync(string id)
        {
            if (_idStore.Exists(id)) 
            {
                await _apiClient.DeleteProductAsync(id);
                _idStore.Remove(id); //delete the id from in memory store as well
            }
            else
            {
                throw new NotFoundException(id);
            }
            
        }
    }
}
