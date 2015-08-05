using Neat.Infrastructure.Security.Attribute;

namespace Neat.Model
{
    public class NeatExample : BaseModel
    {
        public NeatExample()
        {
            Address = new Address();
        }

        [SecureWriteProperty]
        [SecureReadProperty]
        public string Name { get; set; }

        public Address Address { get; set; }
    }
}