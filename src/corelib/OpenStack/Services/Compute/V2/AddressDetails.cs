namespace OpenStack.Services.Compute.V2
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;
    using IPAddressSimpleConverter = net.openstack.Core.Domain.Converters.IPAddressSimpleConverter;

#if PORTABLE
    using IPAddress = System.String;
#else
    using IPAddress = System.Net.IPAddress;
#endif

    /// <summary>
    /// This class models the JSON representation of a server address in the
    /// form used by the <see cref="IComputeService"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class AddressDetails : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Version"/> property.
        /// </summary>
        [JsonProperty("version", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private int? _version;

        /// <summary>
        /// This is the backing field for the <see cref="Address"/> property.
        /// </summary>
        [JsonProperty("addr", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(IPAddressSimpleConverter))]
        private IPAddress _address;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressDetails"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected AddressDetails()
        {
        }

        /// <summary>
        /// Gets the IP address version used by the <see cref="Address"/> property.
        /// </summary>
        /// <value>
        /// <c>4</c> if the address is an IP V4 address.
        /// <para>-or-</para>
        /// <para><c>6</c> if the address is an IP V4 address.</para>
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public int? Version
        {
            get
            {
                return _version;
            }
        }

        /// <summary>
        /// Gets the IP address represented by this <see cref="AddressDetails"/> instance.
        /// </summary>
        /// <value>
        /// An <see cref="IPAddress"/> instance containing the IP address represented by this instance.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public IPAddress Address
        {
            get
            {
                return _address;
            }
        }
    }
}
