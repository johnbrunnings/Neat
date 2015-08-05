using System;
using MongoRepository;

namespace Neat.Infrastructure.Session.Model
{
    public class SessionGrace : IEntity<string>
    {
        public SessionGrace()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public string SessionId { get; set; }
        public DateTime GraceStartDate { get; set; }
        public DateTime GraceExpirationDate { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, SessionId: {1}, GraceStartDate: {2}, GraceExpirationDate: {3}", Id, SessionId, GraceStartDate, GraceExpirationDate);
        }
    }
}