namespace Neat.Infrastructure.Config
{
    public interface IConfig
    {
        //[Cache]
        string GetSetting(string key);

        //[Cache]
        string GetConnectionString(string key);

        //[Cache]
        string GetContainer();
    }
}