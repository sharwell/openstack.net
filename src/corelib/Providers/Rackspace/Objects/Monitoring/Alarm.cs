namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class Alarm : AlarmConfiguration
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Id"/> property.
        /// </summary>
        [JsonProperty("id")]
        private AlarmId _id;
#pragma warning restore 649

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
