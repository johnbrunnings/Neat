using MongoRepository;

namespace Neat.Infrastructure.Security.Model
{
    public class User : IEntity<string>
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}