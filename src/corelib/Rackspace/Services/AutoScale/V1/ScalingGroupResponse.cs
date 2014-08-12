namespace Rackspace.Services.AutoScale.V1
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class ScalingGroupResponse : ExtensibleJsonObject
    {
        [JsonProperty("group", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ScalingGroup _group;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScalingGroupResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ScalingGroupResponse()
        {
        }

        public ScalingGroup Group
        {
            get
            {
                return _group;
            }
        }
    }
}
