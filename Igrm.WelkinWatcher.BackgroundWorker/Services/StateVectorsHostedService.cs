using Igrm.OpenFlights;
using Igrm.OpenSkyApi;
using Igrm.WelkinWatcher.BackgroundWorker.Workers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Igrm.WelkinWatcher.BackgroundWorker.Services
{
    public class StateVectorsHostedService : BackgroundService
    {
        private readonly ILogger<StateVectorsHostedService> _logger;
        private readonly IStateVectorsWorker _stateVectorsWorker;
        private readonly IConfiguration _configuration;

        public StateVectorsHostedService(IConfiguration configuration, ILogger<StateVectorsHostedService> logger, IStateVectorsWorker stateVectorsWorker) :base()
        {
             _logger = logger;
            _stateVectorsWorker = stateVectorsWorker;
            _configuration = configuration;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting.....");
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stoping.....");
            return Task.CompletedTask;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Executing.....");
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("ProduceVectorMessagesAsync called.....");
                await _stateVectorsWorker.ProduceVectorMessagesAsync();
                await Task.Delay(_configuration.GetValue<int>("StateVectorsPeriod"), stoppingToken);
            }

        }
    }
}
