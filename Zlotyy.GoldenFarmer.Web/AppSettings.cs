namespace Zlotyy.GoldenFarmer.Web
{
    public class AppSettings
    {
        public ServiceSettings WebApi { get; set; }
    }

    public class ServiceSettings : UserCredentials
    {
        public string Url { get; set; }
    }

    public class UserCredentials
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
