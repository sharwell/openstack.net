namespace OpenStackNetTests.Live
{
    using System;
    using System.Threading;
    using Newtonsoft.Json;
    using OpenStackNetTests.Unit;
    using OpenStackNetTests.Unit.Simulator.IdentityService.V2;

    public sealed partial class IdentityV2Tests : IDisposable
    {
        private SimulatedIdentityService _simulator;

        internal TestCredentials Credentials
        {
            get
            {
                return JsonConvert.DeserializeObject<TestCredentials>(Resources.SimulatedCredentials);
            }
        }

        public IdentityV2Tests()
        {
            _simulator = new SimulatedIdentityService();
            _simulator.StartAsync(CancellationToken.None);
        }

        public void Dispose()
        {
            _simulator.Dispose();
            _simulator = null;
        }
    }
}
