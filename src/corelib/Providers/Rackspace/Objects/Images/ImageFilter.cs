namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ImageFilter
    {
        private string _name;

        private ImageVisibility _visibility;

        private MemberStatus _memberStatus;

        private string _owner;

        private ImageTag _tag;

        private ImageStatus _status;

        private long? _sizeMin;

        private long? _sizeMax;

        private ImageSortKey _sortKey;

        private ImageSortDirection _sortDirection;

        /// <summary>
        /// Filter parameter that specifies the name of the image as a string.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Filter parameter that specifies image visibility as either <see cref="ImageVisibility.Public"/> or <see cref="ImageVisibility.Private"/>.
        /// </summary>
        public ImageVisibility Visibility
        {
            get
            {
                return _visibility;
            }
        }

        /// <summary>
        /// Filter parameter that shows images with the specified member status. Valid values are <see cref="MemberStatus.Accepted"/>, <see cref="MemberStatus.Pending"/>, <see cref="MemberStatus.Rejected"/>, and <see cref="MemberStatus.All"/>. The default is <see cref="MemberStatus.Accepted"/>.
        /// </summary>
        public MemberStatus MemberStatus
        {
            get
            {
                return _memberStatus;
            }
        }

        /// <summary>
        /// Filter parameter that shows images shared with me by the specified tag.
        /// </summary>
        public string Owner
        {
            get
            {
                return _owner;
            }
        }

        /// <summary>
        /// Filter parameter that shows images with the specified tag, where the owner is indicated by tenant ID.
        /// </summary>
        public ImageTag Tag
        {
            get
            {
                return _tag;
            }
        }

        /// <summary>
        /// Filter parameter that species the image status as <see cref="ImageStatus.Queued"/>, <see cref="ImageStatus.Saving"/>, <see cref="ImageStatus.Active"/>, <see cref="ImageStatus.Killed"/>, <see cref="ImageStatus.Deleted"/>, or <see cref="ImageStatus.PendingDelete"/>.
        /// </summary>
        public ImageStatus Status
        {
            get
            {
                return _status;
            }
        }

        /// <summary>
        /// Filter parameter that specifies the minimum size of the image in bytes.
        /// </summary>
        public long? SizeMin
        {
            get
            {
                return _sizeMin;
            }
        }

        /// <summary>
        /// Filter parameter that specifies the maximum size of the image in bytes.
        /// </summary>
        public long? SizeMax
        {
            get
            {
                return _sizeMax;
            }
        }

        /// <summary>
        /// Sort key. All image attributes can be used as the sort key, except <c>tags</c> and <c>link</c> attributes. The default is <see cref="ImageSortKey.CreatedAt"/>.
        /// </summary>
        public ImageSortKey SortKey
        {
            get
            {
                return _sortKey;
            }
        }

        /// <summary>
        /// Sort direction. Valid values are <see cref="ImageSortDirection.Ascending"/> and <see cref="ImageSortDirection.Descending"/>. The default is <see cref="ImageSortDirection.Descending"/>.
        /// </summary>
        public ImageSortDirection SortDirection
        {
            get
            {
                return _sortDirection;
            }
        }
    }
}
