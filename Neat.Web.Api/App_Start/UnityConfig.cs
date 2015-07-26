namespace Neat.Web.Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			Infrastructure.WebApi.Bootstrapper.Register();
        }
    }
}