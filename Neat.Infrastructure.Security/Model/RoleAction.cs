using System;
using MongoRepository;

namespace Neat.Infrastructure.Security.Model
{
    public class RoleAction : IEntity<string>
    {
        public RoleAction()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public string Role { get; set; }
        public string Action { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, Role: {1}, Action: {2}", Id, Role, Action);
        }
    }
}