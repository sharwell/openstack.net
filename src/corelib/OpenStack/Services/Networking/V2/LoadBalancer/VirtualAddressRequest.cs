namespace OpenStack.Services.Networking.V2.LoadBalancer
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class VirtualAddressRequest : ExtensibleJsonObject
    {
        [JsonProperty("vip", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private VirtualAddressData _virtualAddress;

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualAddressRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected VirtualAddressRequest()
        {
        }

        public VirtualAddressRequest(VirtualAddressData virtualAddress)
        {
            _virtualAddress = virtualAddress;
        }

        public VirtualAddressRequest(VirtualAddressData virtualAddress, params JProperty[] extensionData)
            : base(extensionData)
        {
            _virtualAddress = virtualAddress;
        }

        public VirtualAddressRequest(VirtualAddressData virtualAddress, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _virtualAddress = virtualAddress;
        }

        public VirtualAddressData VirtualAddress
        {
            get
            {
                return _virtualAddress;
            }
        }
    }
}
