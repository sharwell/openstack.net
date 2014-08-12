namespace OpenStack.Services.Images.V2
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class UpdateImageMemberRequest : ExtensibleJsonObject
    {
        [JsonProperty("status", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ImageMemberStatus _status;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateImageMemberRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected UpdateImageMemberRequest()
        {
        }

        public UpdateImageMemberRequest(ImageMemberStatus status)
        {
            _status = status;
        }

        public UpdateImageMemberRequest(ImageMemberStatus status, params JProperty[] extensionData)
            : base(extensionData)
        {
            _status = status;
        }

        public UpdateImageMemberRequest(ImageMemberStatus status, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _status = status;
        }

        public ImageMemberStatus Status
        {
            get
            {
                return _status;
            }
        }
    }
}
