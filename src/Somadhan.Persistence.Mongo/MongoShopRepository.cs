using MongoDB.Driver;

using Somadhan.Domain.Interfaces;

namespace Somadhan.Persistence.Mongo
{
    public class MongoShopRepository : MongoRepository<Shop>, IShopRepository
    {
        public MongoShopRepository(IMongoDatabase database, string? collectionName = null)
            : base(database, collectionName)
        {

        }

    }
}
