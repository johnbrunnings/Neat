using System;
using Neat.Infrastructure.Config;

namespace Neat.Infrastructure.Security.Context
{
    public class SecurityContext : ISecurityContext
    {
        private readonly IConfig _config;

        public SecurityContext(IConfig config)
        {
            _config = config;
        }

        public bool EnableLoginUserOnCreation
        {
            get { return Convert.ToBoolean(_config.GetSetting("Security:EnableLoginUserOnCreation")); }
        }

        public TimeSpan LoginUserOnCreationDuration
        {
            get
            {
                var duration = _config.GetSetting("Security:LoginUserOnCreationDuration");
                var durationParts = duration.Split(new string[] {":"}, StringSplitOptions.RemoveEmptyEntries);
                var days = Int32.Parse(durationParts[0]);
                var hours = Int32.Parse(durationParts[1]);
                var minutes = Int32.Parse(durationParts[2]);
                var seconds = Int32.Parse(durationParts[3]);
                return new TimeSpan(days, hours, minutes, seconds);
            }
        }

        public bool EnableUserLevelSecurity { get { return Convert.ToBoolean(_config.GetSetting("Security:EnableUserLevelSecurity")); } }

        public bool EnableObjectLevelSecurity { get { return Convert.ToBoolean(_config.GetSetting("Security:EnableObjectLevelSecurity")); } }

        public bool EnableFieldLevelSecurity { get { return Convert.ToBoolean(_config.GetSetting("Security:EnableFieldLevelSecurity")); } }

        public int FieldLevelSecurityEvaulationDepth { get { return Convert.ToInt32(_config.GetSetting("Security:FieldLevelSecurityEvaulationDepth")); } }
    }
}