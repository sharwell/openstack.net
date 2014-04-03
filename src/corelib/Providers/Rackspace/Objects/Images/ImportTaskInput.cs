namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using System;
    using Newtonsoft.Json;
    using ExtensibleJsonObject = net.openstack.Core.Domain.ExtensibleJsonObject;

    /// <summary>
    /// This class models the JSON representation of the input parameters for an
    /// <see cref="ImageTaskType.Import"/> task in the <see cref="IImageService"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class ImportTaskInput : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="Properties"/> property.
        /// </summary>
        [JsonProperty("image_properties")]
        private ImageProperties _imageProperties;

        /// <summary>
        /// This is the backing field for the <see cref="ImportFrom"/> property.
        /// </summary>
        [JsonProperty("import_from")]
        private string _importFrom;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportTaskInput"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ImportTaskInput()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportTaskInput"/> class with the specified values.
        /// </summary>
        /// <param name="importFrom">The container and object name of the image in Object Storage.</param>
        /// <param name="imageName">The desired name assigned to the imported image.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="importFrom"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="imageName"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="importFrom"/> is empty.
        /// <para>-or-</para>
        /// <para>If <paramref name="imageName"/> is empty.</para>
        /// </exception>
        public ImportTaskInput(string importFrom, string imageName)
            : this(importFrom, new ImageProperties(imageName))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportTaskInput"/> class with the specified values.
        /// </summary>
        /// <param name="importFrom"></param>
        /// <param name="properties"></param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="importFrom"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="properties"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">If <paramref name="importFrom"/> is empty.</exception>
        public ImportTaskInput(string importFrom, ImageProperties properties)
        {
            if (importFrom == null)
                throw new ArgumentNullException("importFrom");
            if (properties == null)
                throw new ArgumentNullException("properties");
            if (string.IsNullOrEmpty(importFrom))
                throw new ArgumentException("importFrom cannot be empty");

            _imageProperties = properties;
            _importFrom = importFrom;
        }

        /// <summary>
        /// Gets an <see cref="ImageProperties"/> object describing the additional properties for
        /// the <see cref="Image"/> created by the import operation.
        /// </summary>
        public ImageProperties Properties
        {
            get
            {
                return _imageProperties;
            }
        }

        /// <summary>
        /// Gets the source location of the object in the Object Storage service to import as an image.
        /// </summary>
        public string ImportFrom
        {
            get
            {
                return _importFrom;
            }
        }

        /// <summary>
        /// This class models the JSON representation of the additional properties to set
        /// for an image created by an import operation in the <see cref="IImageService"/>.
        /// </summary>
        [JsonObject(MemberSerialization.OptIn)]
        public class ImageProperties
        {
            /// <summary>
            /// This is the backing field for the <see cref="Name"/> property.
            /// </summary>
            [JsonProperty("name")]
            private string _name;

            /// <summary>
            /// Initializes a new instance of the <see cref="ImageProperties"/> class
            /// during JSON deserialization.
            /// </summary>
            [JsonConstructor]
            protected ImageProperties()
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="ImageProperties"/> class
            /// with the specified name.
            /// </summary>
            /// <param name="imageName">The name of the image to create by the import operation.</param>
            /// <exception cref="ArgumentNullException">If <paramref name="imageName"/> is <see langword="null"/>.</exception>
            /// <exception cref="ArgumentException">If <paramref name="imageName"/> is empty.</exception>
            public ImageProperties(string imageName)
            {
                if (imageName == null)
                    throw new ArgumentNullException("imageName");
                if (string.IsNullOrEmpty(imageName))
                    throw new ArgumentException("imageName cannot be empty");

                _name = imageName;
            }

            /// <summary>
            /// Gets the name of the image to created by the import operation.
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
}
