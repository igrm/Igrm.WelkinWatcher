using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igrm.WelkinWatcher.BackgroundWorker.Workers
{
    public interface IStateVectorsWorker
    {
        Task ProduceVectorMessages();
    }

    public class StateVectorsWorker:WorkerBase, IStateVectorsWorker
    {
        private ILogger _logger;

        public StateVectorsWorker(ILogger logger)
        {
            _logger = logger;
        }

        public async Task ProduceVectorMessages()
        {
            await Task.Run(() => _logger.LogDebug("Hello")); ;
        }
    }
}
