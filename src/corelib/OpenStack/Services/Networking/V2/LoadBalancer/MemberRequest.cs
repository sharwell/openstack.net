namespace OpenStack.Services.Networking.V2.LoadBalancer
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class MemberRequest : ExtensibleJsonObject
    {
        [JsonProperty("member", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private MemberData _member;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected MemberRequest()
        {
        }

        public MemberRequest(MemberData member)
        {
            _member = member;
        }

        public MemberRequest(MemberData member, params JProperty[] extensionData)
            : base(extensionData)
        {
            _member = member;
        }

        public MemberRequest(MemberData member, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _member = member;
        }

        public MemberData Member
        {
            get
            {
                return _member;
            }
        }
    }
}
