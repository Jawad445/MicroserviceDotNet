 using MongoDB.Driver;
using Play.Catalog.Entities;

namespace Play.Catalog.Repositories;

public class MongoRepository<T> : IRepository<T> where T : IEntity
{
    private readonly IMongoCollection<T> _dbCollection;
    private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;

    public MongoRepository(IMongoDatabase database, string collectionName)
    {
        _dbCollection = database.GetCollection<T>(collectionName);
    }

    public async Task<IReadOnlyCollection<T>> GetAllAsync()
    {
        return await _dbCollection.Find(filterBuilder.Empty).ToListAsync();
    }

    public async Task<T> GetAsync(Guid Id)
    {
        FilterDefinition<T> filter = filterBuilder.Eq(entity => entity.Id, Id);
        return await _dbCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        await _dbCollection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        FilterDefinition<T> filter = filterBuilder.Eq(existEntity => existEntity.Id, entity.Id);
        await _dbCollection.ReplaceOneAsync(filter, entity);

    }

    public async Task RemoveAsync(Guid Id)
    {
        FilterDefinition<T> filter = filterBuilder.Eq(entity => entity.Id, Id);
        await _dbCollection.DeleteOneAsync(filter);
    }


}
