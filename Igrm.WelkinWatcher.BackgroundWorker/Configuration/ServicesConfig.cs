using Igrm.OpenSkyApi;
using Igrm.OpenFlights;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Igrm.WelkinWatcher.BackgroundWorker.Services;
using Rebus.ServiceProvider;
using Rebus.Config;
using RabbitMQ.Client;
using Igrm.WelkinWatcher.BackgroundWorker.Workers;
using Rebus.Transport.InMem;
using Igrm.OpenSkyApi.Models.Response;
using Rebus.Routing.TypeBased;

namespace Igrm.WelkinWatcher.BackgroundWorker.Configuration
{
    public static class ServicesConfig
    {
        public static Action<HostBuilderContext,IServiceCollection> Execute =>
              (context, collection) =>
              {
                  var connectionFactory = new ConnectionFactory();
                  context.Configuration.GetSection("RabbitMqConnection").Bind(connectionFactory);
                  string connectionString = $"amqp://{connectionFactory.UserName}:{connectionFactory.Password}@{connectionFactory.HostName}:5672{connectionFactory.VirtualHost}";
                  collection.AddLogging();
                  collection.AddHttpClient();
                  collection.AddRebus(configure => configure.Logging(l => l.Serilog())
                                                            .Transport(t => t.UseInMemoryTransport(new InMemNetwork(), "default"))
                                                            .Routing(route=>route.TypeBased().Map<StateVector>("default")));
                  collection.AddTransient<IStateVectorsWorker, StateVectorsWorker>();
                  collection.AddTransient<IHostedService, StateVectorsHostedService>();
                  
              };
    }
}
