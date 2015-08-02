using System;
using System.Linq;
using Neat.Infrastructure.Session.Context;
using Neat.Infrastructure.Session.Model;
using Neat.Infrastructure.Session.Model.Request;
using Neat.Infrastructure.Session.Storage;

namespace Neat.Infrastructure.Session
{
    public class SessionRecyclingSessionProviderDecorator : ISessionProvider
    {
        private readonly ISessionProvider _sessionProvider;
        private readonly ISessionContext _sessionContext;
        private readonly ISessionGraceStorageProvider _sessionGraceStorageProvider;

        public SessionRecyclingSessionProviderDecorator(ISessionProvider sessionProvider, ISessionContext sessionContext, ISessionGraceStorageProvider sessionGraceStorageProvider)
        {
            _sessionProvider = sessionProvider;
            _sessionContext = sessionContext;
            _sessionGraceStorageProvider = sessionGraceStorageProvider;
        }

        public Model.Session CreateSession(string userId, SessionDurationRequest sessionDurationRequest)
        {
            return _sessionProvider.CreateSession(userId, sessionDurationRequest);
        }

        public Model.Session GetSession(string userId)
        {
            return _sessionProvider.GetSession(userId);
        }

        public Model.Session GetCurrentSession()
        {
            return _sessionProvider.GetCurrentSession();
        }

        public void SetCurrentSession(Model.Session session)
        {
            _sessionProvider.SetCurrentSession(session);
        }

        public Model.Session ValidateSession(Model.Session session)
        {
            var now = DateTime.UtcNow;
            var returnSession = _sessionProvider.ValidateSession(session);
            
            if (_sessionContext.EnableSessionRecycling)
            {
                if (returnSession == null)
                {
                    var graceSession = _sessionGraceStorageProvider.GetAll().FirstOrDefault(g => g.SessionId == session.Id && g.GraceStartDate <= now && g.GraceExpirationDate >= now);
                    if (graceSession == null)
                    {
                        return null;
                    }
                    returnSession = _sessionProvider.GetSession(session.UserId);
                    _sessionProvider.SetCurrentSession(returnSession);
                }
                else
                {
                    var currentSessionDuration = now - session.StartDate;
                    if (currentSessionDuration > _sessionContext.SessionRecyclingDuration)
                    {
                        var sessionGraceDuration = _sessionContext.SessionGraceDuration;
                        var graceSessionExpirationDate = now
                                                            .AddDays(sessionGraceDuration.Days)
                                                            .AddHours(sessionGraceDuration.Hours)
                                                            .AddMinutes(sessionGraceDuration.Minutes)
                                                            .AddSeconds(sessionGraceDuration.Seconds);
                        var graceSession = new SessionGrace()
                        {
                            SessionId = session.Id,
                            GraceStartDate = now,
                            GraceExpirationDate = graceSessionExpirationDate
                        };
                        _sessionGraceStorageProvider.Add(graceSession);
                        _sessionProvider.Logout(session.UserId);

                        var remainingSessionDuration = session.ExpirationDate - now;
                        var sessionDurationRequest = new SessionDurationRequest()
                        {
                            Days = remainingSessionDuration.Days,
                            Hours = remainingSessionDuration.Hours,
                            Minutes = remainingSessionDuration.Minutes,
                            Seconds = remainingSessionDuration.Seconds
                        };

                        returnSession = _sessionProvider.CreateSession(session.UserId, sessionDurationRequest);
                    }
                }
            }

            return returnSession;
        }

        public void Logout(string userId)
        {
            _sessionProvider.Logout(userId);
        }
    }
}