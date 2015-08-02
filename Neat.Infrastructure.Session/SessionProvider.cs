using System;
using System.Linq;
using System.Web;
using Neat.Infrastructure.Session.Model.Request;
using Neat.Infrastructure.Session.Storage;

namespace Neat.Infrastructure.Session
{
    public class SessionProvider : ISessionProvider
    {
        private readonly ISessionStorageProvider _sessionStorageProvider;

        public SessionProvider(ISessionStorageProvider sessionStorageProvider)
        {
            _sessionStorageProvider = sessionStorageProvider;
        }

        public Model.Session CreateSession(string userId, SessionDurationRequest sessionDurationRequest)
        {
            var now = DateTime.UtcNow;
            var session = new Model.Session()
            {
                UserId = userId,
                StartDate = now,
                ExpirationDate = now
                                    .AddDays(sessionDurationRequest.Days)
                                    .AddHours(sessionDurationRequest.Hours)
                                    .AddMinutes(sessionDurationRequest.Minutes)
                                    .AddSeconds(sessionDurationRequest.Seconds)
            };

            _sessionStorageProvider.Add(session);
            SetCurrentSession(session);

            return session;
        }

        public Model.Session GetSession(string userId)
        {
            var now = DateTime.UtcNow;
            var session = _sessionStorageProvider.GetAll().FirstOrDefault(s => s.UserId == userId && s.StartDate <= now && s.ExpirationDate >= now);

            return session;
        }

        public Model.Session GetCurrentSession()
        {
            return HttpContext.Current.Items["Session"] as Model.Session;
        }

        public void SetCurrentSession(Model.Session session)
        {
            HttpContext.Current.Items["Session"] = session;
        }

        public Model.Session ValidateSession(Model.Session session)
        {
            var now = DateTime.UtcNow;
            var existingSession = _sessionStorageProvider.GetAll().FirstOrDefault(s => s.Id == session.Id && s.StartDate <= now && s.ExpirationDate >= now);
            if (existingSession != null)
            {
                SetCurrentSession(existingSession);
            }

            return existingSession;
        }

        public void Logout(string userId)
        {
            var now = DateTime.UtcNow;
            var session = _sessionStorageProvider.GetAll().FirstOrDefault(s => s.UserId == userId && s.StartDate >= now && s.ExpirationDate <= now);

            if (session != null)
            {
                session.ExpirationDate = now;
                _sessionStorageProvider.Update(session);
            }
        }
    }
}