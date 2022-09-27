 using MongoDB.Driver;
using Play.Catalog.Entities;

namespace Play.Catalog.Repositories
{
    public class ItemRepository
    {
        private const string collectionName = "items";
        private readonly IMongoCollection<Item> _dbCollection;
        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

        public ItemRepository()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("catelog");
            _dbCollection = database.GetCollection<Item>(collectionName);
        }

        public async Task<IReadOnlyCollection<Item>> GetAllAsync()
        {
            return await _dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<Item> GetAsync(Guid Id)
        {
            FilterDefinition<Item> filter = filterBuilder.Eq(entity => entity.Id, Id);
            return await _dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Item entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await _dbCollection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(Item entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            FilterDefinition<Item> filter = filterBuilder.Eq(existEntity => existEntity.Id, entity.Id);
            await _dbCollection.ReplaceOneAsync(filter, entity);

        }

        public async Task RemoveAsync(Guid Id)
        {
            FilterDefinition<Item> filter = filterBuilder.Eq(entity => entity.Id, Id);
            await _dbCollection.DeleteOneAsync(filter);
        }


    }
}
