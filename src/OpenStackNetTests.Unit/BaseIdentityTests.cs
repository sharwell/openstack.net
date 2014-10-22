namespace OpenStackNetTests.Live
{
    using System;
    using System.Threading;
    using Newtonsoft.Json;
    using OpenStackNetTests.Unit;
    using OpenStackNetTests.Unit.Simulator.IdentityService;

    public partial class BaseIdentityTests : IDisposable
    {
        private SimulatedBaseIdentityService _simulator;

        public BaseIdentityTests()
        {
            TestInitialize();
        }

        internal TestCredentials Credentials
        {
            get
            {
                return JsonConvert.DeserializeObject<TestCredentials>(Resources.SimulatedCredentials);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                TestCleanup();
            }
        }

        public void TestInitialize()
        {
            _simulator = new SimulatedBaseIdentityService(5000);
            _simulator.StartAsync(CancellationToken.None);
        }

        public void TestCleanup()
        {
            _simulator.Dispose();
            _simulator = null;
        }
    }
}
