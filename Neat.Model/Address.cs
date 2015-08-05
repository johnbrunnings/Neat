using Neat.Infrastructure.Security.Attribute;

namespace Neat.Model
{
    public class Address : BaseModel
    {
        [SecureWriteProperty]
        [SecureReadProperty]
        public string Address1 { get; set; }
    }
}