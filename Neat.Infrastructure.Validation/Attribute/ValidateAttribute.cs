using System;

namespace Neat.Infrastructure.Validation.Attribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ValidateAttribute : System.Attribute
    {
        public string Parameters { get; set; }
    }
}