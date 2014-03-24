namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using System;
    using System.Threading;
    using ProjectId = net.openstack.Core.Domain.ProjectId;

    /// <summary>
    /// This class defines a filter for use with <see cref="IImageService.ListImagesAsync(ImageFilter, ImageId, Nullable{int}, CancellationToken)"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class ImageFilter
    {
        /// <summary>
        /// This is the backing field for the <see cref="Name"/> property.
        /// </summary>
        private string _name;

        /// <summary>
        /// This is the backing field for the <see cref="Visibility"/> property.
        /// </summary>
        private ImageVisibility _visibility;

        /// <summary>
        /// This is the backing field for the <see cref="MemberStatus"/> property.
        /// </summary>
        private MemberStatus _memberStatus;

        /// <summary>
        /// This is the backing field for the <see cref="Owner"/> property.
        /// </summary>
        private ProjectId _owner;

        /// <summary>
        /// This is the backing field for the <see cref="Tag"/> property.
        /// </summary>
        private ImageTag _tag;

        /// <summary>
        /// This is the backing field for the <see cref="Status"/> property.
        /// </summary>
        private ImageStatus _status;

        /// <summary>
        /// This is the backing field for the <see cref="SizeMin"/> property.
        /// </summary>
        private long? _sizeMin;

        /// <summary>
        /// This is the backing field for the <see cref="SizeMax"/> property.
        /// </summary>
        private long? _sizeMax;

        /// <summary>
        /// This is the backing field for the <see cref="SortKey"/> property.
        /// </summary>
        private ImageSortKey _sortKey;

        /// <summary>
        /// This is the backing field for the <see cref="SortDirection"/> property.
        /// </summary>
        private ImageSortDirection _sortDirection;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageFilter"/> class with the
        /// specified properties.
        /// </summary>
        /// <param name="name">The name of the image to include; otherwise, <see langword="null"/> if the listing should not be filtered by image name.</param>
        /// <param name="visibility">The visibility of images to include; otherwise, <see langword="null"/> if the listing should not be filtered by visibility.</param>
        /// <param name="memberStatus">The member status of images to include; otherwise, either <see langword="null"/> or <see cref="Images.MemberStatus.All"/> if the listing should not be filtered by member status.</param>
        /// <param name="owner">The project ID of the owner of the image; otherwise, <see langword="null"/> if the listing should not be filtered by owner.</param>
        /// <param name="tag">An <see cref="ImageTag"/> which is included in the <see cref="Image.Tags"/> for listed images; otherwise, <see langword="null"/> if the listing should not be filtered by tag.</param>
        /// <param name="status">The status of images to include in the listing; otherwise, <see langword="null"/> if the listing should not be filtered by image status.</param>
        /// <param name="sizeMin">The minimum size of images to include in the listing; otherwise, <see langword="null"/> if the listing should not be filtered by minimum image size.</param>
        /// <param name="sizeMax">The maximum size of images to include in the listing; otherwise, <see langword="null"/> if the listing should not be filtered by maximum image size.</param>
        /// <param name="sortKey">An <see cref="ImageSortKey"/> instance indicating the key to sort the resulting image list on; otherwise, <see langword="null"/> if the listing should not be sorted.</param>
        /// <param name="sortDirection">An <see cref="ImageSortDirection"/> instance indicating the sort order for the image list; otherwise, <see langword="null"/> if the listing should not be sorted.</param>
        /// <exception cref="ArgumentException">
        /// If <paramref name="sortKey"/> is not <see langword="null"/> and <paramref name="sortDirection"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="sortDirection"/> is not <see langword="null"/> and <paramref name="sortKey"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If <paramref name="sizeMin"/> is less than 0.
        /// <para>-or-</para>
        /// <para>If <paramref name="sizeMax"/> is less than 0.</para>
        /// </exception>
        public ImageFilter(
            string name = null,
            ImageVisibility visibility = null,
            MemberStatus memberStatus = null,
            ProjectId owner = null,
            ImageTag tag = null,
            ImageStatus status = null,
            long? sizeMin = null,
            long? sizeMax = null,
            ImageSortKey sortKey = null,
            ImageSortDirection sortDirection = null)
        {
            if (sizeMin < 0)
                throw new ArgumentOutOfRangeException("sizeMin");
            if (sizeMax < 0)
                throw new ArgumentOutOfRangeException("sizeMax");

            if (sortKey == null)
            {
                if (sortDirection != null)
                    throw new ArgumentException("sortKey must be specified if sortDirection is specified");
            }
            else if (sortDirection == null)
            {
                throw new ArgumentException("sortDirection must be specified if sortKey is specified");
            }

            _name = name;
            _visibility = visibility;
            _memberStatus = memberStatus;
            _owner = owner;
            _tag = tag;
            _status = status;
            _sizeMin = sizeMin;
            _sizeMax = sizeMax;
            _sortKey = sortKey;
            _sortDirection = sortDirection;
        }

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
        /// Filter parameter that shows images with the specified member status. Valid values are <see cref="Images.MemberStatus.Accepted"/>, <see cref="Images.MemberStatus.Pending"/>, <see cref="Images.MemberStatus.Rejected"/>, and <see cref="Images.MemberStatus.All"/>. The default is <see cref="Images.MemberStatus.Accepted"/>.
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
        public ProjectId Owner
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
        /// Sort key. All image attributes can be used as the sort key, except <c>tags</c> and <c>link</c> attributes. The default is <see cref="ImageSortKey.Created"/>.
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
