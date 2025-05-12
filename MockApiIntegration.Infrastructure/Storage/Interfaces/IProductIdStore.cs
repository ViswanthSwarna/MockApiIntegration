namespace MockApiIntegration.Infrastructure.Storage.Interfaces
{
    // Abstraction for product ID storage.
    public interface IProductIdStore
    {
        void Add(string id, string name);
        void Remove(string id);
        bool Exists(string id);
        IReadOnlyCollection<string> GetPagedIds(string nameFilter, int page, int pageSize);
    }
}
