using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igrm.WelkinWatcher.BackgroundWorker.Configuration
{
    public static class LogConfig
    {
        public static Action<HostBuilderContext, ILoggingBuilder> Execute =>
                      (context, builder) =>
                      {

                      }
    }
}
