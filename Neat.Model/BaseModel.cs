using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoRepository;

namespace Neat.Model
{
    [BsonIgnoreExtraElements(Inherited = true)]
    public abstract class BaseModel : IEntity<string>
    {
        public BaseModel()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }
    }
}