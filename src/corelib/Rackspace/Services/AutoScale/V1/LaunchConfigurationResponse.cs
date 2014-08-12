namespace Rackspace.Services.AutoScale.V1
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class LaunchConfigurationResponse : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="LaunchConfiguration"/> property.
        /// </summary>
        [JsonProperty("launchConfiguration", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private LaunchConfiguration _launchConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchConfigurationResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected LaunchConfigurationResponse()
        {
        }

        public LaunchConfiguration LaunchConfiguration
        {
            get
            {
                return _launchConfiguration;
            }
        }
    }
}
