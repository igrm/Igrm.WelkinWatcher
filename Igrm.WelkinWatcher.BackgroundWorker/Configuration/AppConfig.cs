using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igrm.WelkinWatcher.BackgroundWorker.Configuration
{
    public static class AppConfig
    {
        public static Action<HostBuilderContext, IConfigurationBuilder> Execute => 
                      (context, builder) => 
                      {

                      };
    }
}
