namespace OpenStack.Services.Orchestration.V1
{
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class SchemaConstraint : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="AllowedValues"/> property.
        /// </summary>
        [JsonProperty("allowed_values", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private JToken[] _allowedValues;

        /// <summary>
        /// This is the backing field for the <see cref="Length"/> property.
        /// </summary>
        [JsonProperty("length", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private LengthConstraint _length;

        /// <summary>
        /// This is the backing field for the <see cref="Range"/> property.
        /// </summary>
        [JsonProperty("range", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private RangeConstraint _range;

        /// <summary>
        /// This is the backing field for the <see cref="AllowedPattern"/> property.
        /// </summary>
        [JsonProperty("allowed_pattern", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _allowedPattern;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="SchemaConstraint"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected SchemaConstraint()
        {
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

        public LengthConstraint Length
        {
            get
            {
                return _length;
            }
        }

        public RangeConstraint Range
        {
            get
            {
                return _range;
            }
        }

        public string AllowedPattern
        {
            get
            {
                return _allowedPattern;
            }
        }
    }
}
