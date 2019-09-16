using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igrm.WelkinWatcher.BackgroundWorker.Configuration
{
    public static class ServicesConfig
    {
        public static Action<HostBuilderContext,IServiceCollection> Execute =>
              (context, collection) =>
              {

              };
    }
}
