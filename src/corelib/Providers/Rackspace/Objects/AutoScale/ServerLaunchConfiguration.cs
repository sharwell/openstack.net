namespace net.openstack.Providers.Rackspace.Objects.AutoScale
{
    using System;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class ServerLaunchConfiguration : LaunchConfiguration<ServerLaunchArguments>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerLaunchConfiguration"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ServerLaunchConfiguration()
        {
        }

        public ServerLaunchConfiguration(ServerLaunchArguments arguments)
            : base(LaunchType.LaunchServer, arguments)
        {
        }
    }
}
