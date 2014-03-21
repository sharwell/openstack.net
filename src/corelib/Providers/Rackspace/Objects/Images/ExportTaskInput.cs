namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using System;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class ExportTaskInput
    {
        /// <summary>
        /// This is the backing field for the <see cref="ImageId"/> property.
        /// </summary>
        [JsonProperty("image_uuid")]
        private ImageId _imageUuid;

        /// <summary>
        /// This is the backing field for the <see cref="ReceivingContainer"/> property.
        /// </summary>
        [JsonProperty("receiving_swift_container")]
        private string _receivingSwiftContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportTaskInput"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ExportTaskInput()
        {
        }

        public ExportTaskInput(ImageId imageId, string receivingContainer)
        {
            if (imageId == null)
                throw new ArgumentNullException("imageId");
            if (receivingContainer == null)
                throw new ArgumentNullException("receivingContainer");
            if (string.IsNullOrEmpty(receivingContainer))
                throw new ArgumentException("receivingContainer cannot be empty");

            _imageUuid = imageId;
            _receivingSwiftContainer = receivingContainer;
        }

        public ImageId ImageId
        {
            get
            {
                return _imageUuid;
            }
        }

        public string ReceivingContainer
        {
            get
            {
                return _receivingSwiftContainer;
            }
        }
    }
}
