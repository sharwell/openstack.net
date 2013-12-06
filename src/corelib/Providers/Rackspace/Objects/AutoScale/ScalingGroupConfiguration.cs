namespace net.openstack.Providers.Rackspace.Objects.AutoScale
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class ScalingGroupConfiguration : ScalingGroupConfiguration<PolicyConfiguration>
    {
        public ScalingGroupConfiguration(GroupConfiguration groupConfiguration, LaunchConfiguration launchConfiguration, IEnumerable<PolicyConfiguration> scalingPolicies)
            : base(groupConfiguration, launchConfiguration, scalingPolicies)
        {
        }
    }
}
