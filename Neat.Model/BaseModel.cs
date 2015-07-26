using System;
using System.ComponentModel.DataAnnotations;
using MongoRepository;

namespace Neat.Model
{
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