using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igrm.WelkinWatcher.BackgroundWorker.Configuration
{
    public static class HostConfig
    {
        public static Action<IConfigurationBuilder> Execute =>
                     (builder) => 
                     {

                     };
    }
}
