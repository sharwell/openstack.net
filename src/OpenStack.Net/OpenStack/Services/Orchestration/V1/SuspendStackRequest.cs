namespace OpenStack.Services.Orchestration.V1
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class SuspendStackRequest : StackActionRequest
    {
        [JsonProperty("suspend", DefaultValueHandling = DefaultValueHandling.Include)]
        private JToken _suspend;

        /// <summary>
        /// Initializes a new instance of the <see cref="SuspendStackRequest"/> class.
        /// </summary>
        [JsonConstructor]
        public SuspendStackRequest()
        {
        }
    }
}
