namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class ExportTaskResult
    {
        /// <summary>
        /// This is the backing field for the <see cref="ExportLocation"/> property.
        /// </summary>
        [JsonProperty("export_location")]
        private string _exportLocation;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportTaskResult"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ExportTaskResult()
        {
        }

        /// <summary>
        /// Gets the container and object name of the exported image in the Object Storage service.
        /// </summary>
        public string ExportLocation
        {
            get
            {
                return _exportLocation;
            }
        }
    }
}
