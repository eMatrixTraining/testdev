using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;

namespace TestDev
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateAppLogger();
            BuildWebHost(args).Run();
        }

         private static void CreateAppLogger()
        {
            var config = new LoggerConfiguration();
            config.MinimumLevel.Debug();
            config.MinimumLevel.Override("Microsoft", LogEventLevel.Information);
            config.Enrich.FromLogContext();
            config.WriteTo.File("wwwroot/App_Data/Logs/log.txt", rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 300000000);

            Log.Logger = config.CreateLogger();
            Log.Information("Test Dev Started!");

        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseSerilog()
            .UseStartup<Startup>()
            .Build();
    }
}
