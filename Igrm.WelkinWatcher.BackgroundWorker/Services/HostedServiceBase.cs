using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Igrm.WelkinWatcher.BackgroundWorker.Services
{
    public abstract class HostedServiceBase : IHostedService
    {
        protected IConfiguration _configuration;

        public HostedServiceBase(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public abstract Task StartAsync(CancellationToken cancellationToken);

        public abstract Task StopAsync(CancellationToken cancellationToken);
    }
}
