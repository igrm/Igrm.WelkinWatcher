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
    public class StateVectorsHostedService : HostedServiceBase
    {
        private readonly ILogger<StateVectorsHostedService> _logger;
        private readonly IStateVectorsWorker _stateVectorsWorker;

        public StateVectorsHostedService(IConfiguration configuration, ILogger<StateVectorsHostedService> logger, IStateVectorsWorker stateVectorsWorker) :base(configuration)
        {
             _logger = logger;
            _stateVectorsWorker = stateVectorsWorker;
        }

        public async override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting.....");
            await _stateVectorsWorker.ProduceVectorMessages() ;
        }

        public async override Task StopAsync(CancellationToken cancellationToken)
        {

        }
    }
}
