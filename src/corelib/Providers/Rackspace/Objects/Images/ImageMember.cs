namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using net.openstack.Core.Domain;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class ImageMember
    {
        [JsonProperty("image_id")]
        private ImageId _imageId;

        [JsonProperty("member_id")]
        private ProjectId _memberId;

        [JsonProperty("status")]
        private MemberStatus _status;

        [JsonProperty("created_at")]
        private DateTimeOffset? _createdAt;

        [JsonProperty("updated_at")]
        private DateTimeOffset? _updatedAt;

        [JsonProperty("schema")]
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

        public MemberStatus Status
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

        public string Schema
        {
            get
            {
                return _schema;
            }
        }
    }
}
