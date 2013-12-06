namespace net.openstack.Providers.Rackspace.Objects.AutoScale
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [JsonObject(MemberSerialization.OptIn)]
    public abstract class LaunchConfiguration
    {
        public abstract LaunchType LaunchType
        {
            get;
        }

        public static LaunchConfiguration FromJObject(JObject obj)
        {
            JToken launchType = obj["type"];
            if (launchType == null || launchType.ToObject<LaunchType>() == LaunchType.LaunchServer)
                return obj.ToObject<ServerLaunchConfiguration>();

            return obj.ToObject<GenericLaunchConfiguration>();
        }
    }
}
