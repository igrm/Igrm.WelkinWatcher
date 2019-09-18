using Igrm.OpenFlights;
using Igrm.OpenSkyApi;
using Igrm.WelkinWatcher.BackgroundWorker.Workers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Igrm.WelkinWatcher.BackgroundWorker.Services
{
    public class StateVectorsHostedService : HostedServiceBase, IDisposable
    {
        private readonly ILogger<StateVectorsHostedService> _logger;
        private readonly IStateVectorsWorker _stateVectorsWorker;
        private Timer _timer;

        public StateVectorsHostedService(IConfiguration configuration, ILogger<StateVectorsHostedService> logger, IStateVectorsWorker stateVectorsWorker) :base(configuration)
        {
             _logger = logger;
            _stateVectorsWorker = stateVectorsWorker;
        }

        public void Dispose()
        {
            _stateVectorsWorker.Dispose();
            _timer.Dispose();
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting.....");
            TimerCallback callback = new TimerCallback(async (state) => { await _stateVectorsWorker.ProduceVectorMessagesAsync(); });
            _timer = new Timer(callback, null, TimeSpan.Zero, TimeSpan.FromSeconds(25));
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stoping.....");
            _timer.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
