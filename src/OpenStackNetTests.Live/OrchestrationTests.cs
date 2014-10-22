namespace OpenStackNetTests.Live
{
    using System;

    public sealed partial class OrchestrationTests : IDisposable
    {
        private LiveTestConfiguration _configuration;

        internal TestCredentials Credentials
        {
            get
            {
                if (_configuration == null)
                    return null;

                return _configuration.TryGetSelectedCredentials();
            }
        }

        public OrchestrationTests()
        {
            _configuration = LiveTestConfiguration.LoadDefaultConfiguration();
        }

        public void Dispose()
        {
        }
    }
}
