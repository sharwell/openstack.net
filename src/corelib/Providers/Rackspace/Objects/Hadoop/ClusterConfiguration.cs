namespace net.openstack.Providers.Rackspace.Objects.Hadoop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;
    using FlavorId = net.openstack.Providers.Rackspace.Objects.Databases.FlavorId;

    [JsonObject(MemberSerialization.OptIn)]
    public abstract class ClusterConfiguration
    {
        private string _name;

        private ClusterTypeId _clusterType;

        private FlavorId _flavorId;

        private int? _nodeCount;
    }
}
