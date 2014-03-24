namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// This class models the JSON representation of the input parameters for an
    /// <see cref="ImageTaskType.Export"/> task in the <see cref="IImageService"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportTaskInput"/> class for the specified
        /// image ID and target container where the image will be saved.
        /// </summary>
        /// <param name="imageId">The ID of the image to export. This is obtained from <see cref="Image.Id"/>.</param>
        /// <param name="receivingContainer">The name of the container where the image will be saved.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="imageId"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="receivingContainer"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">If <paramref name="receivingContainer"/> is empty.</exception>
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

        /// <summary>
        /// Gets the ID of the image to export.
        /// </summary>
        /// <seealso cref="Image.Id"/>
        public ImageId ImageId
        {
            get
            {
                return _imageUuid;
            }
        }

        /// <summary>
        /// Gets the name of the Object Storage container where the image will be saved.
        /// </summary>
        public string ReceivingContainer
        {
            get
            {
                return _receivingSwiftContainer;
            }
        }
    }
}
