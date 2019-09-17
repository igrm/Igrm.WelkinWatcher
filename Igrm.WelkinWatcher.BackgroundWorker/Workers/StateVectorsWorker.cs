using Igrm.OpenSkyApi;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Igrm.OpenSkyApi.Models.Request;
using Igrm.OpenFlights;
using System.Text.RegularExpressions;

namespace Igrm.WelkinWatcher.BackgroundWorker.Workers
{
    public interface IStateVectorsWorker
    {
        Task ProduceVectorMessages();
    }

    public class StateVectorsWorker:WorkerBase, IStateVectorsWorker
    {
        private ILogger<StateVectorsWorker> _logger;
        private readonly IOpenSkyClient _openSkyClient;
        private readonly IOpenFlightsDataCache _openFlightsDataCache;

        public StateVectorsWorker(ILogger<StateVectorsWorker> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            var client = httpClientFactory.CreateClient();
            _openSkyClient = new OpenSkyClient(client);
            _openFlightsDataCache = new OpenFlightsDataCache(client);
        }

        public async Task ProduceVectorMessages()
        {
       
            var vectors = _openSkyClient.GetAllStateVectors(new AllStateVectorsRequestModel());
            var airlines = await _openFlightsDataCache.GetAirlinesAsync();

            foreach (var item in vectors.StateVectors)
            {
                _logger.LogInformation($"{item.LastContact}, {item.Icao24}, {item.CallSign}, {item.OnGround}, {item.Velocity}");
            }

        }
    }
}
