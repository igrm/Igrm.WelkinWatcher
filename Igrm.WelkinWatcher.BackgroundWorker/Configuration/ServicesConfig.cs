using Igrm.OpenSkyApi.Models.Response;
using Igrm.WelkinWatcher.BackgroundWorker.Services;
using Igrm.WelkinWatcher.BackgroundWorker.Workers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MassTransit.AspNetCoreIntegration;
using System;
using RabbitMQ.Client;
using MassTransit;
using Igrm.OpenSkyApi.Builders;

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
                  collection.AddMassTransit(
                      config => {
                              config.AddBus(
                                  provider =>
                                          Bus.Factory.CreateUsingRabbitMq(busFactoryConfigurator =>
                                          {
                                              var host = busFactoryConfigurator.Host(new Uri($"rabbitmq://{connectionFactory.HostName}:{connectionFactory.Port}"), hostConfigurator =>
                                              {
                                                  hostConfigurator.Username(connectionFactory.UserName);
                                                  hostConfigurator.Password(connectionFactory.Password);
                                              });

                                              busFactoryConfigurator.UseHealthCheck(provider);
                                             
                                          })
                              );
                          }
                  );
                  collection.AddMassTransitHostedService();

                  collection.AddScoped<IStateVectorsWorker, StateVectorsWorker>();
                  collection.AddScoped<IAllStateVectorsRequestBuilder, AllStateVectorsRequestBuilder>();

                  collection.AddHostedService<StateVectorsHostedService>();

              };
    }
}
