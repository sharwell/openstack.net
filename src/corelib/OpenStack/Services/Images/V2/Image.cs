namespace OpenStack.Services.Images.V2
{
    using System;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class Image : ImageData
    {
        /// <summary>
        /// This is the backing field for the <see cref="Id"/> property.
        /// </summary>
        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ImageId _id;

        /// <summary>
        /// This is the backing field for the <see cref="Status"/> property.
        /// </summary>
        [JsonProperty("status", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ImageStatus _status;

        /// <summary>
        /// This is the backing field for the <see cref="Protected"/> property.
        /// </summary>
        [JsonProperty("protected", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private bool? _protected;

        /// <summary>
        /// This is the backing field for the <see cref="Checksum"/> property.
        /// </summary>
        [JsonProperty("checksum", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _checksum;

        /// <summary>
        /// This is the backing field for the <see cref="Size"/> property.
        /// </summary>
        [JsonProperty("size", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private long? _size;

        /// <summary>
        /// This is the backing field for the <see cref="Created"/> property.
        /// </summary>
        [JsonProperty("created_at", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private DateTimeOffset? _created;

        /// <summary>
        /// This is the backing field for the <see cref="LastModified"/> property.
        /// </summary>
        [JsonProperty("updated_at", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private DateTimeOffset? _updated;

        /// <summary>
        /// This is the backing field for the <see cref="Self"/> property.
        /// </summary>
        [JsonProperty("self", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _self;

        /// <summary>
        /// This is the backing field for the <see cref="File"/> property.
        /// </summary>
        [JsonProperty("file", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _file;

        /// <summary>
        /// This is the backing field for the <see cref="Schema"/> property.
        /// </summary>
        [JsonProperty("schema", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _schema;

        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Image()
        {
        }

        public ImageId Id
        {
            get
            {
                return _id;
            }
        }

        public ImageStatus Status
        {
            get
            {
                return _status;
            }
        }

        public bool? Protected
        {
            get
            {
                return _protected;
            }
        }

        public string Checksum
        {
            get
            {
                return _checksum;
            }
        }

        public long? Size
        {
            get
            {
                return _size;
            }
        }

        public DateTimeOffset? Created
        {
            get
            {
                return _created;
            }
        }

        public DateTimeOffset? LastModified
        {
            get
            {
                return _updated;
            }
        }

        public Uri Self
        {
            get
            {
                if (_self == null)
                    return null;

                return new Uri(_self, UriKind.RelativeOrAbsolute);
            }
        }

        public Uri File
        {
            get
            {
                if (_file == null)
                    return null;

                return new Uri(_file, UriKind.RelativeOrAbsolute);
            }
        }

        public Uri Schema
        {
            get
            {
                if (_schema == null)
                    return null;

                return new Uri(_schema, UriKind.RelativeOrAbsolute);
            }
        }
    }
}
