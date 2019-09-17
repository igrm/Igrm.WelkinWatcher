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
        private readonly IOpenSkyClient _openSkyClient;
        private readonly IOpenFlightsDataCache _openFlightsDataCache;
        private readonly ILogger<StateVectorsHostedService> _logger;

        public StateVectorsHostedService(IConfiguration configuration, ILogger<StateVectorsHostedService> logger, IHttpClientFactory clientFactory) :base(configuration)
        {
            var httpClient = clientFactory.CreateClient();

            _openSkyClient = new OpenSkyClient(httpClient);
            _openFlightsDataCache = new OpenFlightsDataCache(httpClient);

            _logger = logger;
        }

        public async override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting.....");
            await new StateVectorsWorker(_logger).ProduceVectorMessages() ;
        }

        public async override Task StopAsync(CancellationToken cancellationToken)
        {

        }
    }
}
