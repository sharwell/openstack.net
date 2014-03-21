namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using System;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class ImportTaskInput
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
        /// 
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

        public ImageProperties Properties
        {
            get
            {
                return _imageProperties;
            }
        }

        public string ImportFrom
        {
            get
            {
                return _importFrom;
            }
        }

        [JsonObject(MemberSerialization.OptIn)]
        public class ImageProperties
        {
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

            public ImageProperties(string imageName)
            {
                if (imageName == null)
                    throw new ArgumentNullException("imageName");
                if (string.IsNullOrEmpty(imageName))
                    throw new ArgumentException("imageName cannot be empty");

                _name = imageName;
            }

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
