namespace net.openstack.Providers.Rackspace.Objects.AutoScale
{
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class GroupState
    {
        [JsonProperty("name")]
        private string _name;

        [JsonProperty("paused")]
        private bool? _paused;

        [JsonProperty("activeCapacity")]
        private long? _activeCapacity;

        [JsonProperty("desiredCapacity")]
        private long? _desiredCapacity;

        [JsonProperty("pendingCapacity")]
        private long? _pendingCapacity;

        [JsonProperty("active")]
        private ActiveServer[] _active;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupState"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected GroupState()
        {
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public bool? Paused
        {
            get
            {
                return _paused;
            }
        }

        public long? ActiveCapacity
        {
            get
            {
                return _activeCapacity;
            }
        }

        public long? DesiredCapacity
        {
            get
            {
                return _desiredCapacity;
            }
        }

        public long? PendingCapacity
        {
            get
            {
                return _pendingCapacity;
            }
        }

        public ReadOnlyCollection<ActiveServer> Active
        {
            get
            {
                if (_active == null)
                    return null;

                return new ReadOnlyCollection<ActiveServer>(_active);
            }
        }
    }
}
