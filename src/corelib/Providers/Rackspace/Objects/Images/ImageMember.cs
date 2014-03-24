namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using System;
    using net.openstack.Core.Domain;
    using Newtonsoft.Json;

    /// <summary>
    /// This class models the JSON representation of an image member in the <see cref="IImageService"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class ImageMember
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="ImageId"/> property.
        /// </summary>
        [JsonProperty("image_id")]
        private ImageId _imageId;

        /// <summary>
        /// This is the backing field for the <see cref="MemberId"/> property.
        /// </summary>
        [JsonProperty("member_id")]
        private ProjectId _memberId;

        /// <summary>
        /// This is the backing field for the <see cref="Status"/> property.
        /// </summary>
        [JsonProperty("status")]
        private MemberStatus _status;

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
        /// This is the backing field for the <see cref="Schema"/> property.
        /// </summary>
        [JsonProperty("schema")]
        private string _schema;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageMember"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ImageMember()
        {
        }

        /// <summary>
        /// Gets the image ID of the shared image.
        /// </summary>
        public ImageId ImageId
        {
            get
            {
                return _imageId;
            }
        }

        /// <summary>
        /// Gets the ID of the member the image is shared with.
        /// </summary>
        public ProjectId MemberId
        {
            get
            {
                return _memberId;
            }
        }

        /// <summary>
        /// Gets the status of the member.
        /// </summary>
        public MemberStatus Status
        {
            get
            {
                return _status;
            }
        }

        /// <summary>
        /// Gets a timestamp indicating when the image member resource was created.
        /// </summary>
        public DateTimeOffset? Created
        {
            get
            {
                return _createdAt;
            }
        }

        /// <summary>
        /// Gets a timestamp indicating when the image member resource was last modified.
        /// </summary>
        public DateTimeOffset? LastModified
        {
            get
            {
                return _updatedAt;
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
    }
}
