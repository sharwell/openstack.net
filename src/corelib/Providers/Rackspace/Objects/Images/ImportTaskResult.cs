namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class ImportTaskResult
    {
        /// <summary>
        /// This is the backing field for the <see cref="ImageId"/> property.
        /// </summary>
        [JsonProperty("image_id")]
        private ImageId _imageId;

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
