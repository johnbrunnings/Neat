using System;
using MongoRepository;

namespace Neat.Infrastructure.Security.Model
{
    public class User : IEntity<string>
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, Username: {1}, Password: {2}, Email: {3}", Id, Username, Password, Email);
        }
    }
}