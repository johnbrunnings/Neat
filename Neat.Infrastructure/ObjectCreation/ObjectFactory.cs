using System;

namespace Neat.Infrastructure.ObjectCreation
{
    public class ObjectFactory : IObjectFactory
    {
        public object Create(Type type)
        {
            if (type == typeof(string))
            {
                return string.Empty;
            }
            return Activator.CreateInstance(type);
        }
    }
}