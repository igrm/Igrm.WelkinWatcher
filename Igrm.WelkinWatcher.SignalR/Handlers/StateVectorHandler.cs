using Igrm.OpenSkyApi.Models.Response;
using Rebus.Handlers;
using Rebus.Retry.Simple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igrm.WelkinWatcher.SignalR.Handlers
{
    public class StateVectorHandler : IHandleMessages<StateVector>, IHandleMessages<IFailed<StateVector>>
    {
        public StateVectorHandler()
        {

        }

        public Task Handle(StateVector message)
        {
            throw new NotImplementedException();
        }

        public Task Handle(IFailed<StateVector> message)
        {
            throw new NotImplementedException();
        }
    }
}
