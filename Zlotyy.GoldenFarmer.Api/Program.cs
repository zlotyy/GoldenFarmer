using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Zlotyy.GoldenFarmer.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var _config = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

            var builder = Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
            //.ConfigureLogging(logging =>
            //{
            //    logging.ClearProviders();
            //    logging.SetMinimumLevel(LogLevel.Trace);
            //})
            //.UseContentRoot(AppDomain.CurrentDomain.BaseDirectory)
            //.UseNLog();

            return builder;
        }
    }
}
