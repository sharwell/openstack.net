namespace OpenStackNetTests.Live
{
    using System;

    public sealed partial class BaseIdentityTests : IDisposable
    {
        private LiveTestConfiguration _configuration;

        internal TestCredentials Credentials
        {
            get
            {
                TestCredentials credentials = _configuration.TryGetSelectedCredentials();
                if (credentials == null)
                    credentials = _configuration.TryGetCredentials("TryStack_Anonymous");

                return credentials;
            }
        }

        public BaseIdentityTests()
        {
            _configuration = LiveTestConfiguration.LoadDefaultConfiguration();
        }

        public void Dispose()
        {
        }
    }
}
