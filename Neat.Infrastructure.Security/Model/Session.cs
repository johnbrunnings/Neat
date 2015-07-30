using System;
using MongoRepository;

namespace Neat.Infrastructure.Security.Model
{
    public class Session : IEntity<string>
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}