using System;

namespace Neat.Infrastructure.Session.Context
{
    public interface ISessionContext
    {
        bool EnableSessionRecycling { get; }
        TimeSpan SessionRecyclingDuration { get; }
        TimeSpan SessionGraceDuration { get; }
    }
}