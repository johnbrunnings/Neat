namespace Neat.Infrastructure.Security.Model.Request
{
    public class UserAuthenticationAccessTokenRequest
    {
        public string AccessToken { get; set; }

        public override string ToString()
        {
            return string.Format("AccessToken: {0}", AccessToken);
        }
    }
}