using Igrm.OpenSkyApi.Models.Response;
using Igrm.WelkinWatcher.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Rebus.ServiceProvider;
using Serilog;

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

            services.AddSignalR();
            services.AddLogging();
            services.AddRebus(configure => configure.Logging(l => l.Serilog())
                                                    .Routing(r => r.TypeBased().MapAssemblyOf<StateVector>("default"))
                                                    .Transport(t => t.UseRabbitMq(connectionString, "default")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(_config).CreateLogger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSignalR(routes =>
            {
                routes.MapHub<StateVectorsHub>("/stateVectors");
            });

            app.ApplicationServices.UseRebus(async bus => {
                await bus.Subscribe<StateVector>();
            });
        }
    }
}
