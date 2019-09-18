using Igrm.OpenFlights;
using Igrm.OpenSkyApi;
using Microsoft.Extensions.Logging;
using Rebus.Bus;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using Igrm.OpenSkyApi.Models.Request;
using System.Collections.Concurrent;
using System.Linq;

namespace Igrm.WelkinWatcher.BackgroundWorker.Workers
{
    public interface IStateVectorsWorker: IDisposable
    {
        Task ProduceVectorMessagesAsync();
    }

    public class StateVectorsWorker:WorkerBase, IStateVectorsWorker
    {
        private ILogger<StateVectorsWorker> _logger;
        private readonly IBus _bus;
        private readonly IHttpClientFactory _httpClientFactory;

        public StateVectorsWorker(ILogger<StateVectorsWorker> logger, IHttpClientFactory httpClientFactory, IBus bus)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _bus = bus;
        }

        public async Task ProduceVectorMessagesAsync()
        {
            _logger.LogInformation($"-----------------------------{Guid.NewGuid()}----------------------------------------");

            var client = _httpClientFactory.CreateClient();

            var openSkyClient = new OpenSkyClient(client);

            var vectors = openSkyClient.GetAllStateVectors(new AllStateVectorsRequestModel());
            var tasks = new ConcurrentDictionary<string, Task>();

            Parallel.ForEach(vectors.StateVectors.ToArray(), (vector) => {
                tasks.TryAdd(vector.Icao24,
                                Task.Run(() =>{
                                    _logger.LogInformation($"{vector.Icao24}");
                                }));
            });

            await Task.WhenAll(tasks.Values);
        }


        public void Dispose()
        {
        }
    }
}
