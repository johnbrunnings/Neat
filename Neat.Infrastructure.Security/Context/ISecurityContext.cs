using System;

namespace Neat.Infrastructure.Security.Context
{
    public interface ISecurityContext
    {
        bool EnableLoginUserOnCreation { get; }
        TimeSpan LoginUserOnCreationDuration { get; }
        bool EnableUserLevelSecurity { get; }
        bool EnableObjectLevelSecurity { get; }
        bool EnableFieldLevelSecurity { get; }
    }
}