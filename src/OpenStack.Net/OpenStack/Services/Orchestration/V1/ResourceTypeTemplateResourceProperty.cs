namespace OpenStack.Services.Orchestration.V1
{
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class ResourceTypeTemplateResourceProperty : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This value is a key into the <see cref="ResourceTypeTemplate.Parameters"/> dictionary.
        /// </summary>
        [JsonProperty("Ref", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _ref;

        /// <summary>
        /// This array has two elements. The first is a string, and the second is an instance of
        /// <see cref="ResourceTypeTemplateResourceProperty"/>.
        /// </summary>
        [JsonProperty("Fn::Split", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private JToken[] _fnSplit;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceTypeTemplateResourceProperty"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ResourceTypeTemplateResourceProperty()
        {
        }

        public string Ref
        {
            get
            {
                return _ref;
            }
        }

        public ReadOnlyCollection<JToken> FnSplit
        {
            get
            {
                if (_fnSplit == null)
                    return null;

                return new ReadOnlyCollection<JToken>(_fnSplit);
            }
        }
    }
}
