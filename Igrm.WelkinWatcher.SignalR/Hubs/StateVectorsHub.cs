using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Igrm.WelkinWatcher.SignalR.Hubs
{
    public class StateVectorsHub : Hub
    {
        private readonly ILogger<StateVectorsHub> _logger;

        public StateVectorsHub(ILogger<StateVectorsHub> logger)
        {
            _logger = logger;
        }
    }
}
