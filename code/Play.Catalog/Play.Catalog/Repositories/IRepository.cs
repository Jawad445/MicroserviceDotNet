using Play.Catalog.Entities;

namespace Play.Catalog.Repositories;

public interface IRepository<T> where T : IEntity
{
    Task CreateAsync(T entity);
    Task<IReadOnlyCollection<T>> GetAllAsync();
    Task<T> GetAsync(Guid Id);
    Task RemoveAsync(Guid Id);
    Task UpdateAsync(T entity);
}