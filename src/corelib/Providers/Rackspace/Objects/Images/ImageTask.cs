namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using System;
    using Newtonsoft.Json;
    using ProjectId = net.openstack.Core.Domain.ProjectId;

    /// <summary>
    /// This is the base class for objects modeling specific types of tasks in the
    /// <see cref="IImageService"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject]
    public abstract class ImageTask
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
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
        private ProjectId _owner;

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
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageTask"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ImageTask()
        {
        }

        /// <summary>
        /// Gets the unique ID of this task resource.
        /// </summary>
        public ImageTaskId Id
        {
            get
            {
                return _id;
            }
        }

        /// <summary>
        /// Gets a time stamp indicating when this task was created.
        /// </summary>
        public DateTimeOffset? Created
        {
            get
            {
                return _createdAt;
            }
        }

        /// <summary>
        /// Gets a time stamp indicating when this task was last updated.
        /// </summary>
        public DateTimeOffset? LastModified
        {
            get
            {
                return _updatedAt;
            }
        }

        /// <summary>
        /// Gets a time stamp indicating when this task resource expires.
        /// </summary>
        public DateTimeOffset? Expires
        {
            get
            {
                return _expiresAt;
            }
        }

        /// <summary>
        /// Gets a message describing the current status of the task.
        /// </summary>
        public string Message
        {
            get
            {
                return _message;
            }
        }

        /// <summary>
        /// Gets the ID of the owner of this task.
        /// </summary>
        public ProjectId Owner
        {
            get
            {
                return _owner;
            }
        }

        /// <summary>
        /// Gets a <see cref="Uri"/> for the location of the json-schema representation of this object.
        /// </summary>
        public Uri Schema
        {
            get
            {
                if (_schema == null)
                    return null;

                return new Uri(_schema, UriKind.RelativeOrAbsolute);
            }
        }

        /// <summary>
        /// Gets a <see cref="Uri"/> for the location of this object.
        /// </summary>
        public Uri Self
        {
            get
            {
                if (_self == null)
                    return null;

                return new Uri(_self, UriKind.RelativeOrAbsolute);
            }
        }

        /// <summary>
        /// Gets an <see cref="ImageTaskStatus"/> instance indicating the current status of the task.
        /// </summary>
        public ImageTaskStatus Status
        {
            get
            {
                return _status;
            }
        }

        /// <summary>
        /// Gets an <see cref="ImageTaskType"/> instance indicating the task type.
        /// </summary>
        public ImageTaskType Type
        {
            get
            {
                return _type;
            }
        }
    }
}
