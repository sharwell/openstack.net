namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class DiskInformation
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        [JsonProperty("read_bytes")]
        private long? _readBytes;

        [JsonProperty("reads")]
        private long? _readCount;

        [JsonProperty("rtime")]
        private long? _readTime;

        [JsonProperty("write_bytes")]
        private long? _writeBytes;

        [JsonProperty("writes")]
        private long? _writeCount;

        [JsonProperty("wtime")]
        private long? _writeTime;

        [JsonProperty("time")]
        private long? _time;

        [JsonProperty("name")]
        private string _name;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="DiskInformation"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected DiskInformation()
        {
        }

        /// <summary>
        /// Gets the total number of bytes read from disk.
        /// </summary>
        public long? ReadBytes
        {
            get
            {
                return _readBytes;
            }
        }

        /// <summary>
        /// Gets the total number of completed read requests.
        /// </summary>
        public long? ReadCount
        {
            get
            {
                return _readCount;
            }
        }

        /// <summary>
        /// Gets the total time spent reading from disk.
        /// </summary>
        public TimeSpan? ReadTime
        {
            get
            {
                if (_readTime == null)
                    return null;

                return TimeSpan.FromMilliseconds(_readTime.Value);
            }
        }

        /// <summary>
        /// Gets the total number of bytes written to disk.
        /// </summary>
        public long? WriteBytes
        {
            get
            {
                return _writeBytes;
            }
        }

        /// <summary>
        /// Gets the total number of completed write requests.
        /// </summary>
        public long? WriteCount
        {
            get
            {
                return _writeCount;
            }
        }

        /// <summary>
        /// Gets the total time spent writing to disk.
        /// </summary>
        public TimeSpan? WriteTime
        {
            get
            {
                if (_writeTime == null)
                    return null;

                return TimeSpan.FromMilliseconds(_writeTime.Value);
            }
        }

        /// <summary>
        /// Gets the total time spent on disk I/O operations.
        /// </summary>
        public TimeSpan? IOTime
        {
            get
            {
                if (_time == null)
                    return null;

                return TimeSpan.FromMilliseconds(_time.Value);
            }
        }

        /// <summary>
        /// Gets the device name.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
        }
    }
}
