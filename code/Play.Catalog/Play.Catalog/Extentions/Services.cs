using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Play.Catalog.Entities;
using Play.Catalog.Repositories;

namespace Play.Catalog.Extentions;

public static class Services
{    
    public static IServiceCollection AddMongo(this IServiceCollection services)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
        BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));

        
        services.AddSingleton(serviceProvider => {
            var configuration = serviceProvider.GetService<IConfiguration>();
            var serviceSettings = configuration.GetSection("ServiceSettings").Get<ServiceSettings>();
            var mongoDbSettings = configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
            var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);
            return mongoClient.GetDatabase(serviceSettings.Name);
        });

        return services;
    }

    public static IServiceCollection AddMongoRepository<T>(this IServiceCollection services, string collectionName)
        where T : IEntity
    {
        services.AddSingleton<IRepository<T>>(serviceProvider =>
        {
            var database = serviceProvider.GetService<IMongoDatabase>();
            return new MongoRepository<T>(database, collectionName);
        });
        
        return services;

    }
}
