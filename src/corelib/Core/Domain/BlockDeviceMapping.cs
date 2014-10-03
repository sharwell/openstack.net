namespace net.openstack.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [JsonObject(MemberSerialization.OptIn)]
    public class BlockDeviceMapping : ExtensibleJsonObject
    {
        [JsonProperty("source_type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private BlockDeviceSourceType _sourceType;

        [JsonProperty("uuid", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _uuid;

        [JsonProperty("destination_type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private BlockDeviceType _destinationType;

        [JsonProperty("boot_index", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private int? _bootIndex;

        [JsonProperty("delete_on_termination", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private bool? _deleteOnTermination;

        private JToken _deviceName;
        private JToken _deviceType;
        private JToken _volumeSize;
        private JToken _diskBus;
        private JToken _noDevice;
        private JToken _guestFormat;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlockDeviceMapping"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected BlockDeviceMapping()
        {
        }

        public BlockDeviceMapping(BlockDeviceSourceType sourceType, string sourceId, BlockDeviceType destinationType, int? bootIndex = null, bool? deleteOnTermination = null)
        {
            _sourceType = sourceType;
            _uuid = sourceId;
            _destinationType = destinationType;
            _bootIndex = bootIndex;
            _deleteOnTermination = deleteOnTermination;
        }

        public BlockDeviceSourceType SourceType
        {
            get
            {
                return _sourceType;
            }
        }

        public BlockDeviceType DestinationType
        {
            get
            {
                return _destinationType;
            }
        }

        public string Id
        {
            get
            {
                return _uuid;
            }
        }

        public int? BootIndex
        {
            get
            {
                return _bootIndex;
            }
        }

        public bool? DeleteOnTermination
        {
            get
            {
                return _deleteOnTermination;
            }
        }
    }
}
