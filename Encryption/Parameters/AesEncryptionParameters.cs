using Neat.Encryption.Parameters.Abstract;

namespace Neat.Encryption.Parameters
{
    public class AesEncryptionParameters : EncryptionParameters
    {
        public byte[] Key { get; set; }
        public byte[] Vector { get; set; }
    }
}