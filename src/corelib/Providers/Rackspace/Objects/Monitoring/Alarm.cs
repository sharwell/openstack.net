namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class Alarm : AlarmConfiguration
    {
        /// <summary>
        /// This is the backing field for the <see cref="Id"/> property.
        /// </summary>
        [JsonProperty("id")]
        private AlarmId _id;

        /// <summary>
        /// Initializes a new instance of the <see cref="Alarm"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Alarm()
        {
        }

        /// <summary>
        /// Gets the unique identifier for this alarm.
        /// </summary>
        public AlarmId Id
        {
            get
            {
                return _id;
            }
        }
    }
}
