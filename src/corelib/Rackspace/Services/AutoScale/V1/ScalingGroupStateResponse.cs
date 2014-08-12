namespace Rackspace.Services.AutoScale.V1
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class ScalingGroupStateResponse : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="ScalingGroupState"/> property.
        /// </summary>
        /// <remarks>
        /// Yes, the underlying property is actually called <c>group</c>.
        /// </remarks>
        [JsonProperty("group", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ScalingGroupState _scalingGroupState;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScalingGroupStateResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ScalingGroupStateResponse()
        {
        }

        public ScalingGroupState ScalingGroupState
        {
            get
            {
                return _scalingGroupState;
            }
        }
    }
}
