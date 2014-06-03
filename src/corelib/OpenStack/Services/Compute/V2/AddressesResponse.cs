namespace OpenStack.Services.Compute.V2
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation used by the <see cref="GetServerAddressesApiCall"/>
    /// API call.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class AddressesResponse : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Addresses"/> property.
        /// </summary>
        [JsonProperty("addresses", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Addresses _addresses;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressesResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected AddressesResponse()
        {
        }

        /// <summary>
        /// Gets detailed information about IP addresses associated with particular named networks.
        /// </summary>
        /// <value>
        /// An <see cref="Addresses"/> instance containing detailed information about IP addresses.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public Addresses Addresses
        {
            get
            {
                return _addresses;
            }
        }
    }
}
