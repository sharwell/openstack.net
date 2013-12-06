namespace net.openstack.Providers.Rackspace.Objects.AutoScale
{
    using System;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public abstract class LaunchConfiguration<TArguments> : LaunchConfiguration
    {
        [JsonProperty("type")]
        private LaunchType _launchType;

        [JsonProperty("args")]
        private TArguments _arguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchConfiguration"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected LaunchConfiguration()
        {
        }

        protected LaunchConfiguration(LaunchType launchType, TArguments arguments)
        {
            if (launchType == null)
                throw new ArgumentNullException("launchType");

            _launchType = launchType;
            _arguments = arguments;
        }

        public override LaunchType LaunchType
        {
            get
            {
                return _launchType;
            }
        }

        public TArguments Arguments
        {
            get
            {
                return _arguments;
            }
        }
    }
}
