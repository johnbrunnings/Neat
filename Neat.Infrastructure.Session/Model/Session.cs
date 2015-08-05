using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoRepository;
using Newtonsoft.Json;

namespace Neat.Infrastructure.Session.Model
{
    public class Session : IEntity<string>
    {
        public Session()
        {
            Id = Guid.NewGuid().ToString();
            InstantiationDate = DateTime.UtcNow;
        }

        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        [JsonIgnore]
        [BsonIgnore]
        public DateTime InstantiationDate { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, UserId: {1}, StartDate: {2}, ExpirationDate: {3}, InstantiationDate: {4}", Id, UserId, StartDate, ExpirationDate, InstantiationDate);
        }
    }
}