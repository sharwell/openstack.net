namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class Metric
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Name"/> property.
        /// </summary>
        [JsonProperty("name")]
        private MetricName _name;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="Metric"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Metric()
        {
        }

        /// <summary>
        /// Gets the name of the metric.
        /// </summary>
        public MetricName Name
        {
            get
            {
                return _name;
            }
        }
    }
}
