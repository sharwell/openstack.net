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

        public ImportTaskInput(string name, string importFrom)
            : this(new ImageProperties(name), importFrom)
        {
        }

        public ImportTaskInput(ImageProperties properties, string importFrom)
        {
            if (properties == null)
                throw new ArgumentNullException("properties");
            if (importFrom == null)
                throw new ArgumentNullException("importFrom");
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

            public ImageProperties(string name)
            {
                if (name == null)
                    throw new ArgumentNullException("name");
                if (string.IsNullOrEmpty(name))
                    throw new ArgumentException("name cannot be empty");

                _name = name;
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
