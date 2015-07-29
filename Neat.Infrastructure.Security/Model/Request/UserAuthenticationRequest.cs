namespace Neat.Infrastructure.Security.Model.Request
{
    public class UserAuthenticationRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public UserAuthorizationDurationRequest Duration { get; set; }

        public override string ToString()
        {
            return string.Format("Username: {0}, Password: {1}, Duration: {2}", Username, Password, Duration);
        }
    }
}