using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace Igrm.WelkinWatcher.BackgroundWorker
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                          .UseConsoleLifetime()
                          .Build();

            await host.RunAsync();
        }
    }
}
