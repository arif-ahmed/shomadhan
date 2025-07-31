using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Somadhan.Persistence.Mongo
{
    public static class ShopMapper
    {
        public static MongoShop ToMongo(Shop shop) => new()
        {
            Id = shop.Id,
            IsDeleted = shop.IsDeleted,
            Name = shop.Name
            // map other properties
        };

        public static Shop ToDomain(MongoShop mongoShop) => new()
        {
            Id = mongoShop.Id,
            IsDeleted = mongoShop.IsDeleted,
            Name = mongoShop.Name
            // map other properties
        };
    }
}
