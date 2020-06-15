using Igrm.OpenSkyApi.Models.Response;
using Igrm.WelkinWatcher.SignalR.Hubs;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igrm.WelkinWatcher.SignalR
{
    public class StateVectorHandler : IConsumer<StateVector>
    {
        private readonly IHubContext<StateVectorsHub> _hubcontext;
        private readonly ILogger<StateVectorHandler> _logger;

        public StateVectorHandler(IHubContext<StateVectorsHub> hubcontext, ILogger<StateVectorHandler> logger)
        {
            _hubcontext = hubcontext;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<StateVector> context)
        {
            _logger.LogInformation($"Message arrived for {context.Message.Icao24}");
            await _hubcontext.Clients.All.SendAsync("ReceiveStateVector", context.Message);
        }
    }
}
