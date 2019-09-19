using MassTransit;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Igrm.WelkinWatcher.BackgroundWorker.Services
{
    public class BusHostedService : HostedServiceBase
    {
        private readonly IBusControl _busControl;

        public BusHostedService(IConfiguration configuration, IBusControl busControl) : base(configuration)
        {
            _busControl = busControl;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return _busControl.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return _busControl.StopAsync(cancellationToken);
        }
    }
}
