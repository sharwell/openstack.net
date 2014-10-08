namespace OpenStack.Services.Orchestration.V1
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class ResourceTypeTemplateOutput : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Description"/> property.
        /// </summary>
        [JsonProperty("Description", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _description;

        /// <summary>
        /// This is the backing field for the <see cref="Value"/> property.
        /// </summary>
        [JsonProperty("Value", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _value;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceTypeTemplateOutput"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ResourceTypeTemplateOutput()
        {
        }

        public string Description
        {
            get
            {
                return _description;
            }
        }

        public string Value
        {
            get
            {
                return _value;
            }
        }
    }
}
