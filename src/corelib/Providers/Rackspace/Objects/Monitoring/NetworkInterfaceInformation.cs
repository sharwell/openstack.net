namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System.Net;
    using System.Net.NetworkInformation;
    using net.openstack.Core.Domain.Converters;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class NetworkInterfaceInformation
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        [JsonProperty("name")]
        private string _name;

        [JsonProperty("type")]
        private string _type;

        [JsonProperty("address")]
        [JsonConverter(typeof(IPAddressSimpleConverter))]
        private IPAddress _address;

        [JsonProperty("address6")]
        [JsonConverter(typeof(IPAddressSimpleConverter))]
        private IPAddress _address6;

        [JsonProperty("broadcast")]
        [JsonConverter(typeof(IPAddressSimpleConverter))]
        private IPAddress _broadcast;

        [JsonProperty("hwaddr")]
        [JsonConverter(typeof(PhysicalAddressSimpleConverter))]
        private PhysicalAddress _physicalAddress;

        [JsonProperty("netmask")]
        private string _netmask;

        [JsonProperty("mtu")]
        private int? _mtu;

        [JsonProperty("tx_bytes")]
        private long? _transmitBytes;

        [JsonProperty("tx_packets")]
        private long? _transmitPackets;

        [JsonProperty("tx_errors")]
        private long? _transmitErrors;

        [JsonProperty("tx_overruns")]
        private long? _transmitOverruns;

        [JsonProperty("tx_carrier")]
        private long? _transmitCarrierErrors;

        [JsonProperty("tx_collisions")]
        private long? _transmitCollisions;

        [JsonProperty("tx_dropped")]
        private long? _transmitDropped;

        [JsonProperty("rx_bytes")]
        private long? _receiveBytes;

        [JsonProperty("rx_packets")]
        private long? _receivePackets;

        [JsonProperty("rx_errors")]
        private long? _receiveErrors;

        [JsonProperty("rx_overruns")]
        private long? _receiveOverruns;

        [JsonProperty("rx_frame")]
        private long? _receiveInvalidFrames;

        [JsonProperty("rx_dropped")]
        private long? _receiveDropped;

        [JsonProperty("flags")]
        private long? _flags;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkInterfaceInformation"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected NetworkInterfaceInformation()
        {
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string Type
        {
            get
            {
                return _type;
            }
        }

        public IPAddress IPAddress
        {
            get
            {
                return _address;
            }
        }

        public IPAddress IPAddressV6
        {
            get
            {
                return _address6;
            }
        }

        public IPAddress Broadcast
        {
            get
            {
                return _broadcast;
            }
        }

        public PhysicalAddress PhysicalAddress
        {
            get
            {
                return _physicalAddress;
            }
        }

        public string NetworkMask
        {
            get
            {
                return _netmask;
            }
        }

        public int? MaximumTransmissionUnit
        {
            get
            {
                return _mtu;
            }
        }

        public long? TransmitBytes
        {
            get
            {
                return _transmitBytes;
            }
        }

        public long? TransmitPackets
        {
            get
            {
                return _transmitPackets;
            }
        }

        public long? TransmitErrors
        {
            get
            {
                return _transmitErrors;
            }
        }

        public long? TransmitOverruns
        {
            get
            {
                return _transmitOverruns;
            }
        }

        public long? TransmitCarrierErrors
        {
            get
            {
                return _transmitCarrierErrors;
            }
        }

        public long? TransmitCollisions
        {
            get
            {
                return _transmitCollisions;
            }
        }

        public long? TransmitDropped
        {
            get
            {
                return _transmitDropped;
            }
        }

        public long? ReceiveBytes
        {
            get
            {
                return _receiveBytes;
            }
        }

        public long? ReceivePackets
        {
            get
            {
                return _receivePackets;
            }
        }

        public long? ReceiveErrors
        {
            get
            {
                return _receiveErrors;
            }
        }

        public long? ReceiveOverruns
        {
            get
            {
                return _receiveOverruns;
            }
        }

        public long? ReceiveInvalidFrames
        {
            get
            {
                return _receiveInvalidFrames;
            }
        }

        public long? ReceiveDropped
        {
            get
            {
                return _receiveDropped;
            }
        }

        /// <summary>
        /// Gets the operating system-specific interface flags.
        /// </summary>
        public long? Flags
        {
            get
            {
                return _flags;
            }
        }
    }
}
