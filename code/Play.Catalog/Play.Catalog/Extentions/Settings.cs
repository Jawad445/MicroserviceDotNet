namespace Play.Catalog.Extentions;

public class MongoDbSettings
{
    public string Host { get; set; }
    public string Port { get; set; }
    public string ConnectionString => $"mongodb://{Host}:{Port}";
}

public class ServiceSettings
{
    public string Name { get; set; }
}