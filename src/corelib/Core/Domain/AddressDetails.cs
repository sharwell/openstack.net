using System.Runtime.Serialization;
using net.openstack.Core.Providers;

namespace net.openstack.Core.Domain
{
    /// <summary>
    /// Represents a network address in a format compatible with communication with the Compute Service APIs.
    /// </summary>
    /// <seealso cref="IComputeProvider.ListAddresses"/>
    /// <seealso cref="IComputeProvider.ListAddressesByNetwork"/>
    [DataContract]
    public class AddressDetails
    {
        /// <summary>
        /// Gets the network address. This is an IPv4 address if <see cref="Version"/> is <c>"4"</c>,
        /// or an IPv6 address if <see cref="Version"/> is <c>"6"</c>.
        /// </summary>
        [DataMember(Name = "addr")]
        public string Address { get; private set; }

        /// <summary>
        /// Gets the network address version. The value is either <c>"4"</c> or <c>"6"</c>.
        /// </summary>
        [DataMember]
        public string Version { get; private set; }
    }
}
