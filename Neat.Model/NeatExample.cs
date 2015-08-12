using System.Collections.Generic;
using Neat.Infrastructure.Security.Attribute;

namespace Neat.Model
{
    public class NeatExample : BaseModel
    {
        public NeatExample()
        {
            Address = new Address();
            Addresses = new List<Address>();
            //Names = new List<string>();
        }

        [SecureWriteProperty]
        [SecureReadProperty]
        public string Name { get; set; }

        public Address Address { get; set; }

        public List<Address> Addresses { get; set; }

        public List<string> Names { get; set; }
    }
}