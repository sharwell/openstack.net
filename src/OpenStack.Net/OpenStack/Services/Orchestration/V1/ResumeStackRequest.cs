namespace OpenStack.Services.Orchestration.V1
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class ResumeStackRequest : StackActionRequest
    {
        [JsonProperty("resume", DefaultValueHandling = DefaultValueHandling.Include)]
        private JToken _resume;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResumeStackRequest"/> class.
        /// </summary>
        [JsonConstructor]
        public ResumeStackRequest()
        {
        }
    }
}
