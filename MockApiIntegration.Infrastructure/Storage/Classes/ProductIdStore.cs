using MockApiIntegration.Infrastructure.Storage.Interfaces;
using System.Collections.Concurrent;

namespace MockApiIntegration.Infrastructure.Storage.Classes
{
    // In-memory store for product IDs and names to facilitate filtering and pagination.
    public class ProductIdStore : IProductIdStore
    {
        private readonly ConcurrentDictionary<string, string> _products = new();

        public void Add(string id, string name)
        {
            _products.TryAdd(id, name);
        }

        public void Remove(string id)
        {
            _products.TryRemove(id, out _);
        }

        public bool Exists(string id)
        {
            return _products.TryGetValue(id, out _);
        }

        public IReadOnlyCollection<string> GetPagedIds(string nameFilter, int page, int pageSize)
        {
            var filtered = string.IsNullOrWhiteSpace(nameFilter)
                ? _products
                : _products.Where(x => x.Value.Contains(nameFilter, StringComparison.OrdinalIgnoreCase));

            return filtered
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => x.Key)
                .ToList()
                .AsReadOnly();
        }
    }
}
