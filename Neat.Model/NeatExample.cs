using Neat.Infrastructure.Security.Attribute;

namespace Neat.Model
{
    public class NeatExample : BaseModel
    {
        [SecureWriteProperty]
        [SecureReadProperty]
        public string Name { get; set; }
    }
}