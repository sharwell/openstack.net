namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class Image
    {
        [JsonProperty("id")]
        private ImageId _id;

        [JsonProperty("name")]
        private string _name;

        [JsonProperty("status")]
        private ImageStatus _status;

        [JsonProperty("visibility")]
        private ImageVisibility _visibility;

        [JsonProperty("size")]
        private long? _size;

        [JsonProperty("checksum")]
        private string _checksum;

        [JsonProperty("tags")]
        private ImageTag[] _tags;

        [JsonProperty("created")]
        private DateTimeOffset? _created;

        [JsonProperty("updated")]
        private DateTimeOffset? _updated;

        [JsonProperty("self")]
        private string _self;

        [JsonProperty("file")]
        private string _file;

        [JsonProperty("schema")]
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

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public ImageStatus Status
        {
            get
            {
                return _status;
            }
        }

        public ImageVisibility Visibility
        {
            get
            {
                return _visibility;
            }
        }

        public long? Size
        {
            get
            {
                return _size;
            }
        }

        public string Checksum
        {
            get
            {
                return _checksum;
            }
        }

        public ReadOnlyCollection<ImageTag> Tags
        {
            get
            {
                if (_tags == null)
                    return null;

                return new ReadOnlyCollection<ImageTag>(_tags);
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

        public string Self
        {
            get
            {
                return _self;
            }
        }

        public string File
        {
            get
            {
                return _file;
            }
        }

        public string Schema
        {
            get
            {
                return _schema;
            }
        }
    }
}
