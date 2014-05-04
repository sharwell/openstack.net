namespace OpenStack.Services.Networking.V2.Layer3
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class RemoveRouterInterfaceRequest : ExtensibleJsonObject
    {
        [JsonProperty("subnet_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private SubnetId _subnetId;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveRouterInterfaceRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected RemoveRouterInterfaceRequest()
        {
        }

        public RemoveRouterInterfaceRequest(SubnetId subnetId)
        {
            _subnetId = subnetId;
        }

        public RemoveRouterInterfaceRequest(SubnetId subnetId, params JProperty[] extensionData)
            : base(extensionData)
        {
            _subnetId = subnetId;
        }

        public RemoveRouterInterfaceRequest(SubnetId subnetId, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _subnetId = subnetId;
        }

        public SubnetId SubnetId
        {
            get
            {
                return _subnetId;
            }
        }
    }
}
