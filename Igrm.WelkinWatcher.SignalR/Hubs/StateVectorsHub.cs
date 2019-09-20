using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Igrm.OpenSkyApi.Models.Response;

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
