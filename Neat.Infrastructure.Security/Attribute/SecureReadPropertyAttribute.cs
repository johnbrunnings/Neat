namespace Neat.Infrastructure.Security.Attribute
{
    public class SecureReadPropertyAttribute : System.Attribute
    {
        public string PropertyName { get; set; }
    }
}