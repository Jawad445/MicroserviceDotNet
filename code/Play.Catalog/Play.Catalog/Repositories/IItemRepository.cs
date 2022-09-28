using Play.Catalog.Entities;

namespace Play.Catalog.Repositories;

public interface IItemRepository
{
    Task CreateAsync(Item entity);
    Task<IReadOnlyCollection<Item>> GetAllAsync();
    Task<Item> GetAsync(Guid Id);
    Task RemoveAsync(Guid Id);
    Task UpdateAsync(Item entity);
}