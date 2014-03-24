namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using Newtonsoft.Json;

    /// <summary>
    /// This class models the JSON representation of the result of an import task in the <see cref="IImageService"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class ImportTaskResult
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="ImageId"/> property.
        /// </summary>
        [JsonProperty("image_id")]
        private ImageId _imageId;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportTaskResult"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ImportTaskResult()
        {
        }

        /// <summary>
        /// Gets the image ID for the newly imported image.
        /// </summary>
        public ImageId ImageId
        {
            get
            {
                return _imageId;
            }
        }
    }
}
