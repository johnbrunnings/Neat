namespace Neat.Infrastructure.Security.Attribute
{
    public class SecureWritePropertyAttribute : System.Attribute
    {
        public string PropertyName { get; set; }
    }
}