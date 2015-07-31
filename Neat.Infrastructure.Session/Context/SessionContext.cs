using System;
using Neat.Infrastructure.Config;

namespace Neat.Infrastructure.Session.Context
{
    public class SessionContext : ISessionContext
    {
        private readonly IConfig _config;

        public SessionContext(IConfig config)
        {
            _config = config;
        }

        public bool EnableSessionRecycling
        {
            get { return Convert.ToBoolean(_config.GetSetting("Session:EnableSessionRecycling")); }
        }

        public TimeSpan SessionRecyclingDuration
        {
            get
            {
                var duration = _config.GetSetting("Session:SessionRecyclingDuration");
                var durationParts = duration.Split(new string[] {":"}, StringSplitOptions.RemoveEmptyEntries);
                var days = Int32.Parse(durationParts[0]);
                var hours = Int32.Parse(durationParts[1]);
                var minutes = Int32.Parse(durationParts[2]);
                var seconds = Int32.Parse(durationParts[3]);
                return new TimeSpan(days, hours, minutes, seconds);
            }
        }

        public TimeSpan SessionGraceDuration
        {
            get
            {
                var duration = _config.GetSetting("Session:SessionGraceDuration");
                var durationParts = duration.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                var days = Int32.Parse(durationParts[0]);
                var hours = Int32.Parse(durationParts[1]);
                var minutes = Int32.Parse(durationParts[2]);
                var seconds = Int32.Parse(durationParts[3]);
                return new TimeSpan(days, hours, minutes, seconds);
            }
        }
    }
}