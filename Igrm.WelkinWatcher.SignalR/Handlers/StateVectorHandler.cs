using Igrm.OpenSkyApi.Models.Response;
using Igrm.WelkinWatcher.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using Rebus.Bus;
using Rebus.Handlers;
using Rebus.Retry.Simple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igrm.WelkinWatcher.SignalR
{
    public class StateVectorHandler : IHandleMessages<StateVector>
    {
        private readonly IHubContext<StateVectorsHub> _hubcontext;
        public StateVectorHandler(IHubContext<StateVectorsHub> hubcontext)
        {
            _hubcontext = hubcontext;
        }

        public async Task Handle(StateVector message)
        {
            await _hubcontext.Clients.All.SendAsync("ReceiveStateVector", message);
        }
    }
}
