namespace OpenStack.Services.Networking.V2
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class SubnetRequest : ExtensibleJsonObject
    {
        [JsonProperty("subnet", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private SubnetData _subnet;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubnetRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected SubnetRequest()
        {
        }

        public SubnetRequest(SubnetData subnet)
        {
            _subnet = subnet;
        }

        public SubnetRequest(SubnetData subnet, params JProperty[] extensionData)
            : base(extensionData)
        {
            _subnet = subnet;
        }

        public SubnetRequest(SubnetData subnet, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _subnet = subnet;
        }

        public SubnetData Subnet
        {
            get
            {
                return _subnet;
            }
        }
    }
}
