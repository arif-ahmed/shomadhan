using MongoDB.Bson;
using MongoDB.Driver;

namespace Somadhan.API.Seed
{
    public static class MongoDbSeeder
    {
        public static async Task SeedAsync(IMongoDatabase database, CancellationToken cancellationToken = default)
        {
            var shopCollection = database.GetCollection<Shop>("Shops");

            // Check if data already exists
            var existing = await shopCollection.Find(_ => true).AnyAsync(cancellationToken);
            if (existing)
                return;

            var shops = new List<Shop>
        {
            new Shop { Id = ObjectId.GenerateNewId().ToString(), Name = "Tech Plaza", IsDeleted = false },
            new Shop { Id = ObjectId.GenerateNewId().ToString(), Name = "Grocery Mart", IsDeleted = false },
            new Shop { Id = ObjectId.GenerateNewId().ToString(), Name = "Book Corner", IsDeleted = false }
        };

            await shopCollection.InsertManyAsync(shops, cancellationToken: cancellationToken);
        }
    }
}
