using Igrm.OpenSkyApi;
using Igrm.OpenSkyApi.Builders;
using Igrm.OpenSkyApi.Models.Request;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Threading.Tasks;

namespace Igrm.WelkinWatcher.BackgroundWorker.Workers
{
    public interface IStateVectorsWorker
    {
        Task ProduceVectorMessagesAsync();
    }

    public class StateVectorsWorker : WorkerBase, IStateVectorsWorker
    {
        private ILogger<StateVectorsWorker> _logger;
        private readonly IBus _bus;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAllStateVectorsRequestBuilder _requestBuilder;

        public StateVectorsWorker(ILogger<StateVectorsWorker> logger, IHttpClientFactory httpClientFactory, IBus bus, IAllStateVectorsRequestBuilder requestBuilder)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _bus = bus;
            _requestBuilder = requestBuilder;
        }

        public async Task ProduceVectorMessagesAsync()
        {
            _logger.LogInformation("Producing messages...");
            using var client = _httpClientFactory.CreateClient();
            
            var openSkyClient = new OpenSkyClient(client);
            var response = await openSkyClient.GetAllStateVectorsAsync(_requestBuilder.Build());
            
            _logger.LogInformation($"Response received: {response.StateVectors.Count} items.");

            var tasks = new ConcurrentDictionary<string, Task>();

            Parallel.ForEach(response.StateVectors.ToArray(), (vector) =>
            {
                tasks.TryAdd(vector.Icao24, _bus.Publish(vector));
            });

            await Task.WhenAll(tasks.Values);
        }
    }
}

