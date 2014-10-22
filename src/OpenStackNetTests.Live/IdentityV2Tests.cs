namespace OpenStackNetTests.Live
{
    using System;

    public sealed partial class IdentityV2Tests : IDisposable
    {
        private LiveTestConfiguration _configuration;

        internal TestCredentials Credentials
        {
            get
            {
                return _configuration.TryGetSelectedCredentials();
            }
        }

        public IdentityV2Tests()
        {
            _configuration = LiveTestConfiguration.LoadDefaultConfiguration();
        }

        public void Dispose()
        {
        }
    }
}
