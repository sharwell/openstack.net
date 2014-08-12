namespace Rackspace.Services.Images.V2
{
    using System;
    using Newtonsoft.Json;
    using OpenStack.Services.Identity;

    [JsonObject(MemberSerialization.OptIn)]
    public class ImageTask<TInput> : ImageTaskData<TInput>
    {
        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ImageTaskId _id;

        [JsonProperty("status", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ImageTaskStatus _status;

        [JsonProperty("created_at", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private DateTimeOffset? _created;

        [JsonProperty("updated_at", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private DateTimeOffset? _updated;

        [JsonProperty("owner", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ProjectId _owner;

        [JsonProperty("message", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _message;

        /// <summary>
        /// This is the backing field for the <see cref="Self"/> property.
        /// </summary>
        [JsonProperty("self", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _self;

        /// <summary>
        /// This is the backing field for the <see cref="Schema"/> property.
        /// </summary>
        [JsonProperty("schema", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _schema;

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

        public ImageTaskStatus Status
        {
            get
            {
                return _status;
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

        public ProjectId Owner
        {
            get
            {
                return _owner;
            }
        }

        public string Message
        {
            get
            {
                return _message;
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
