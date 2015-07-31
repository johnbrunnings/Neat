using System;
using MongoRepository;

namespace Neat.Infrastructure.Session.Model
{
    public class Session : IEntity<string>
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, UserId: {1}, StartDate: {2}, ExpirationDate: {3}", Id, UserId, StartDate, ExpirationDate);
        }
    }
}