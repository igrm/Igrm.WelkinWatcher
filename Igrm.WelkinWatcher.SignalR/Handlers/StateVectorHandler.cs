using Igrm.OpenSkyApi.Models.Response;
using Igrm.WelkinWatcher.SignalR.Hubs;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igrm.WelkinWatcher.SignalR
{
    public class StateVectorHandler : IConsumer<StateVector>
    {
        private readonly IHubContext<StateVectorsHub> _hubcontext;
        public StateVectorHandler(IHubContext<StateVectorsHub> hubcontext)
        {
            _hubcontext = hubcontext;
        }

        public async Task Consume(ConsumeContext<StateVector> context)
        {
            await _hubcontext.Clients.All.SendAsync("ReceiveStateVector", context.Message);
        }
    }
}
