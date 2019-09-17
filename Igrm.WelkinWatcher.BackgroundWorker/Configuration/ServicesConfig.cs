﻿using Igrm.OpenSkyApi;
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

namespace Igrm.WelkinWatcher.BackgroundWorker.Configuration
{
    public static class ServicesConfig
    {
        public static Action<HostBuilderContext,IServiceCollection> Execute =>
              (context, collection) =>
              {
                  var connectionFactory = new ConnectionFactory();
                  context.Configuration.GetSection("RabbitMqConnection").Bind(connectionFactory);

                  collection.AddLogging();
                  collection.AddHttpClient();
                  collection.AddRebus(configure => configure.Logging(l => l.Serilog())
                                                            .Transport(t=>t.UseRabbitMq(connectionFactory.Uri.ToString(),"default")));
                  collection.AddTransient<IHostedService, StateVectorsHostedService>();
                  
              };
    }
}
