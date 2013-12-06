namespace net.openstack.Providers.Rackspace.Objects.AutoScale
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [JsonObject(MemberSerialization.OptIn)]
    public class GenericLaunchConfiguration : LaunchConfiguration<JToken>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericLaunchConfiguration"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected GenericLaunchConfiguration()
        {
        }

        public GenericLaunchConfiguration(LaunchType launchType, object arguments)
            : base(launchType, JToken.FromObject(arguments))
        {
            if (launchType == null)
                throw new ArgumentNullException("launchType");
        }
    }
}
