using System;

namespace Neat.Infrastructure.ObjectCreation
{
    public interface IObjectFactory
    {
        object Create(Type type);
    }
}