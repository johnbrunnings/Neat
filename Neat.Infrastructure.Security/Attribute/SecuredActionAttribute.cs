using System;

namespace Neat.Infrastructure.Security.Attribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class SecuredActionAttribute : System.Attribute
    {
        public string Action { get; set; }
        public string Parameters { get; set; }
    }
}