namespace OpenStack.Services.Images.V2
{
    using System;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;
    using OpenStack.Services.Identity;

    [JsonObject(MemberSerialization.OptIn)]
    public class ImageMember : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="ImageId"/> property.
        /// </summary>
        [JsonProperty("image_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ImageId _imageId;

        /// <summary>
        /// This is the backing field for the <see cref="MemberId"/> property.
        /// </summary>
        [JsonProperty("member_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ProjectId _memberId;

        /// <summary>
        /// This is the backing field for the <see cref="Status"/> property.
        /// </summary>
        [JsonProperty("status", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ImageMemberStatus _status;

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
        /// This is the backing field for the <see cref="Schema"/> property.
        /// </summary>
        [JsonProperty("schema", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _schema;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageMember"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ImageMember()
        {
        }

        public ImageId ImageId
        {
            get
            {
                return _imageId;
            }
        }

        public ProjectId MemberId
        {
            get
            {
                return _memberId;
            }
        }

        public ImageMemberStatus Status
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
