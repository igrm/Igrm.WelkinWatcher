using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using Serilog;


namespace Igrm.WelkinWatcher.SignalR
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Debug.WriteLine("Starting application...");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseSerilog();
                webBuilder.UseStartup<Startup>();
            });

    }
}
