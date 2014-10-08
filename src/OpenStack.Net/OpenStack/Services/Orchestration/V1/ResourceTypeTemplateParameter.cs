namespace OpenStack.Services.Orchestration.V1
{
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class ResourceTypeTemplateParameter : ExtensibleJsonObject
    {
        [JsonProperty("Description", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _description;

#warning TODO: make this an extensible enum
        [JsonProperty("Type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _type;

        [JsonProperty("Default", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private JToken _default;

        [JsonProperty("MinValue", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private int? _minValue;

        [JsonProperty("MinLength", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private int? _minLength;

        [JsonProperty("MaxLength", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private int? _maxLength;

        [JsonProperty("AllowedValues", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private JToken[] _allowedValues;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceTypeTemplateParameter"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ResourceTypeTemplateParameter()
        {
        }

        public string Description
        {
            get
            {
                return _description;
            }
        }

        public string Type
        {
            get
            {
                return _type;
            }
        }

        public JToken Default
        {
            get
            {
                return _default;
            }
        }

        public int? MinValue
        {
            get
            {
                return _minValue;
            }
        }

        public int? MinLength
        {
            get
            {
                return _minLength;
            }
        }

        public int? MaxLength
        {
            get
            {
                return _maxLength;
            }
        }

        public ReadOnlyCollection<JToken> AllowedValues
        {
            get
            {
                if (_allowedValues == null)
                    return null;

                return new ReadOnlyCollection<JToken>(_allowedValues);
            }
        }
    }
}
