using Igrm.WelkinWatcher.SignalR.Hubs;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using Serilog;
using System;

namespace Igrm.WelkinWatcher.SignalR
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionFactory = new ConnectionFactory();
            _config.GetSection("RabbitMqConnection").Bind(connectionFactory);
            string connectionString = $"amqp://{connectionFactory.UserName}:{connectionFactory.Password}@{connectionFactory.HostName}:5672{connectionFactory.VirtualHost}";

            services.AddCors(options => options.AddPolicy("CorsPolicy",
                                                                builder =>
                                                                {
                                                                    builder.AllowAnyMethod().AllowAnyHeader()
                                                                           .AllowAnyOrigin();
                                                                })
            );
            services.AddSignalR();
            services.AddLogging();
            services.AddMassTransit(
                config =>
                {
                    config.AddConsumer<StateVectorHandler>();
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


                                    busFactoryConfigurator.ReceiveEndpoint("state-vector-queue", ep =>
                                    {
                                        ep.ConfigureConsumer<StateVectorHandler>(provider);
                                    });
                                })
                    );
                }
            );
            services.AddMassTransitHostedService();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(_config).CreateLogger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("CorsPolicy");
            
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<StateVectorsHub>("/stateVectors");
            });


        }
    }
}
