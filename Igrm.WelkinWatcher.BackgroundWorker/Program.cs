using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Igrm.WelkinWatcher.BackgroundWorker.Configuration;

namespace Igrm.WelkinWatcher.BackgroundWorker
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                          .ConfigureHostConfiguration(HostConfig.Execute)
                          .ConfigureAppConfiguration(AppConfig.Execute)
                          .ConfigureServices(ServicesConfig.Execute)
                          .ConfigureLogging(LogConfig.Execute)
                          .UseConsoleLifetime()
                          .Build();

            await host.RunAsync();
        }
    }
}
