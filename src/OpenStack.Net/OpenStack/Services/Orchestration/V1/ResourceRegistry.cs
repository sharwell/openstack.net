namespace OpenStack.Services.Orchestration.V1
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class ResourceRegistry : ExtensibleJsonObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceRegistry"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ResourceRegistry()
        {
        }
    }
}
