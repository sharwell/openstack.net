namespace OpenStackNetTests.Live
{
    using System;

    public sealed partial class ObjectStorageTests : IDisposable
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

        public ObjectStorageTests()
        {
            _configuration = LiveTestConfiguration.LoadDefaultConfiguration();
        }

        public void Dispose()
        {
        }
    }
}
