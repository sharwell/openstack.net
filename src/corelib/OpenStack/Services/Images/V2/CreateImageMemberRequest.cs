namespace OpenStack.Services.Images.V2
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;
    using OpenStack.Services.Identity;

    [JsonObject(MemberSerialization.OptIn)]
    public class CreateImageMemberRequest : ExtensibleJsonObject
    {
        [JsonProperty("member", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ProjectId _memberId;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateImageMemberRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected CreateImageMemberRequest()
        {
        }

        public CreateImageMemberRequest(ProjectId memberId)
        {
            _memberId = memberId;
        }

        public CreateImageMemberRequest(ProjectId memberId, params JProperty[] extensionData)
            : base(extensionData)
        {
            _memberId = memberId;
        }

        public CreateImageMemberRequest(ProjectId memberId, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _memberId = memberId;
        }

        public ProjectId MemberId
        {
            get
            {
                return _memberId;
            }
        }
    }
}
