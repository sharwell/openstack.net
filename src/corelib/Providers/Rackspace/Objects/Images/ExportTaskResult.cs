namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using Newtonsoft.Json;

    /// <summary>
    /// This class models the JSON representation of the result of an export task in the <see cref="IImageService"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class ExportTaskResult
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="ExportLocation"/> property.
        /// </summary>
        [JsonProperty("export_location")]
        private string _exportLocation;
#pragma warning restore 649

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
