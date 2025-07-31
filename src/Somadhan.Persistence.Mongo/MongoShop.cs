using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Somadhan.Persistence.Mongo;

[MongoCollection("Shops")]
public class MongoShop
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = default!;

    public bool IsDeleted
    {
        get; set;
    }
    public string Name { get; set; } = default!;

    // ...other mapped properties
}

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class MongoCollectionAttribute : Attribute
{
    public string CollectionName
    {
        get;
    }

    public MongoCollectionAttribute(string collectionName)
    {
        CollectionName = collectionName;
    }
}
