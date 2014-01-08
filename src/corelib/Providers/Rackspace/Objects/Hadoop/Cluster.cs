namespace net.openstack.Providers.Rackspace.Objects.Hadoop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using net.openstack.Core.Domain;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class Cluster : ClusterConfiguration
    {
        [JsonProperty("id")]
        private ClusterId _id;

        [JsonProperty("created")]
        private DateTimeOffset? _created;

        [JsonProperty("updated")]
        private DateTimeOffset? _updated;

        [JsonProperty("postInitScriptStatus")]
        private string _postInitScriptStatus;

        [JsonProperty("progress")]
        private double? _progress;

        [JsonProperty("")]
        private ClusterStatus _status;

        [JsonProperty("links")]
        private Link[] _links;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cluster"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Cluster()
        {
        }
    }
}
