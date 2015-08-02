using Neat.Infrastructure.Session.Model.Request;

namespace Neat.Infrastructure.Session
{
    public interface ISessionProvider
    {
        Model.Session CreateSession(string userId, SessionDurationRequest sessionDurationRequest);
        Model.Session GetSession(string userId);
        Model.Session GetCurrentSession();
        void SetCurrentSession(Model.Session session);
        Model.Session ValidateSession(Model.Session session);
        void Logout(string userId);
    }
}