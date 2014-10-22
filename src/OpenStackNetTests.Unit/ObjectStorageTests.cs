namespace OpenStackNetTests.Live
{
    using System;
    using System.Threading;
    using Newtonsoft.Json;
    using OpenStackNetTests.Unit;
    using OpenStackNetTests.Unit.Simulator.IdentityService.V2;
    using OpenStackNetTests.Unit.Simulator.ObjectStorageService.V1;
    using Xunit;

    public sealed partial class ObjectStorageTests : IUseFixture<ObjectStorageTests.ObjectStorageFixture>
    {
        internal TestCredentials Credentials
        {
            get
            {
                return JsonConvert.DeserializeObject<TestCredentials>(Resources.SimulatedCredentials);
            }
        }

        public void SetFixture(ObjectStorageFixture data)
        {
        }

        public class ObjectStorageFixture : IDisposable
        {
            private SimulatedIdentityService _identityService;
            private SimulatedObjectStorageService _objectStorageService;

            public ObjectStorageFixture()
            {
                _identityService = new SimulatedIdentityService();
                _identityService.StartAsync(CancellationToken.None);

                _objectStorageService = new SimulatedObjectStorageService(_identityService);
                _objectStorageService.StartAsync(CancellationToken.None);
            }

            public void Dispose()
            {
                _identityService.Dispose();
                _identityService = null;

                _objectStorageService.Dispose();
                _objectStorageService = null;
            }
        }
    }
}
