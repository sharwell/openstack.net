namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using System;
    using Newtonsoft.Json;

    [JsonObject]
    public abstract class ImageTask
    {
        /// <summary>
        /// This is the backing field for the <see cref="Id"/> property.
        /// </summary>
        [JsonProperty("id")]
        private ImageTaskId _id;

        /// <summary>
        /// This is the backing field for the <see cref="Created"/> property.
        /// </summary>
        [JsonProperty("created_at")]
        private DateTimeOffset? _createdAt;

        /// <summary>
        /// This is the backing field for the <see cref="LastModified"/> property.
        /// </summary>
        [JsonProperty("updated_at")]
        private DateTimeOffset? _updatedAt;

        /// <summary>
        /// This is the backing field for the <see cref="Expires"/> property.
        /// </summary>
        [JsonProperty("expires_at")]
        private DateTimeOffset? _expiresAt;

        /// <summary>
        /// This is the backing field for the <see cref="Message"/> property.
        /// </summary>
        [JsonProperty("message")]
        private string _message;

        /// <summary>
        /// This is the backing field for the <see cref="Owner"/> property.
        /// </summary>
        [JsonProperty("owner")]
        private string _owner;

        /// <summary>
        /// This is the backing field for the <see cref="Schema"/> property.
        /// </summary>
        [JsonProperty("schema")]
        private string _schema;

        /// <summary>
        /// This is the backing field for the <see cref="Self"/> property.
        /// </summary>
        [JsonProperty("self")]
        private string _self;

        /// <summary>
        /// This is the backing field for the <see cref="Status"/> property.
        /// </summary>
        [JsonProperty("status")]
        private ImageTaskStatus _status;

        /// <summary>
        /// This is the backing field for the <see cref="Type"/> property.
        /// </summary>
        [JsonProperty("type")]
        private ImageTaskType _type;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageTask"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ImageTask()
        {
        }

        public ImageTaskId Id
        {
            get
            {
                return _id;
            }
        }

        public DateTimeOffset? Created
        {
            get
            {
                return _createdAt;
            }
        }

        public DateTimeOffset? LastModified
        {
            get
            {
                return _updatedAt;
            }
        }

        public DateTimeOffset? Expires
        {
            get
            {
                return _expiresAt;
            }
        }

        public string Message
        {
            get
            {
                return _message;
            }
        }

        public string Owner
        {
            get
            {
                return _owner;
            }
        }

        public string Schema
        {
            get
            {
                return _schema;
            }
        }

        public string Self
        {
            get
            {
                return _self;
            }
        }

        public ImageTaskStatus Status
        {
            get
            {
                return _status;
            }
        }

        public ImageTaskType Type
        {
            get
            {
                return _type;
            }
        }
    }
}
