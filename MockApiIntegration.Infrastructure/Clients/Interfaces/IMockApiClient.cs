using MockApiIntegration.Domain.Models;

namespace MockApiIntegration.Infrastructure.Clients.Interfaces
{
    // Client interface to abstract external mock API calls.
    public interface IMockApiClient
    {
        Task<string> CreateProductAsync(MockApiProduct product);
        Task<List<MockApiProduct>> GetProductsByIdsAsync(IEnumerable<string> ids);
        Task DeleteProductAsync(string id);
    }
}
