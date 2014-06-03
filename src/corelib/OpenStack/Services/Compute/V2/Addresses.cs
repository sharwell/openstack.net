namespace OpenStack.Services.Compute.V2
{
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class Addresses : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Public"/> property.
        /// </summary>
        [JsonProperty("public", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private AddressDetails[] _public;

        /// <summary>
        /// This is the backing field for the <see cref="Private"/> property.
        /// </summary>
        [JsonProperty("private", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private AddressDetails[] _private;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="Addresses"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Addresses()
        {
        }

        public ReadOnlyCollection<AddressDetails> Public
        {
            get
            {
                if (_public == null)
                    return null;

                return new ReadOnlyCollection<AddressDetails>(_public);
            }
        }

        public ReadOnlyCollection<AddressDetails> Private
        {
            get
            {
                if (_private == null)
                    return null;

                return new ReadOnlyCollection<AddressDetails>(_private);
            }
        }
    }
}
