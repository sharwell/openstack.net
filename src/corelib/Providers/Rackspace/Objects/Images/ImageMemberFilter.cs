namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using System.Collections.Generic;
    using System.Threading;
    using ExtensibleJsonObject = net.openstack.Core.Domain.ExtensibleJsonObject;
    using ProjectId = net.openstack.Core.Domain.ProjectId;

    /// <summary>
    /// This class defines a filter for use with <see cref="IImageService.ListImageMembersAsync(ImageId, ImageMemberFilter, CancellationToken)"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class ImageMemberFilter : ExtensibleJsonObject
    {
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
        /// Initializes a new instance of the <see cref="ImageMemberFilter"/> class with the
        /// specified properties.
        /// </summary>
        /// <param name="visibility">The visibility of images to include; otherwise, <see langword="null"/> if the listing should not be filtered by visibility.</param>
        /// <param name="memberStatus">The member status of images to include; otherwise, either <see langword="null"/> or <see cref="Images.MemberStatus.All"/> if the listing should not be filtered by member status.</param>
        /// <param name="owner">The project ID of the owner of the image; otherwise, <see langword="null"/> if the listing should not be filtered by owner.</param>
        public ImageMemberFilter(
            ImageVisibility visibility = null,
            MemberStatus memberStatus = null,
            ProjectId owner = null)
        {
            _visibility = visibility;
            _memberStatus = memberStatus;
            _owner = owner;
        }

        /// <summary>
        /// Filter parameter that specifies image visibility as <see cref="ImageVisibility.Shared"/>, <see cref="ImageVisibility.Public"/>, or <see cref="ImageVisibility.Private"/>.
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
        /// Filter parameter that shows images shared with me by the specified owner.
        /// </summary>
        public ProjectId Owner
        {
            get
            {
                return _owner;
            }
        }

        /// <summary>
        /// Gets a collection of custom query parameters defined by this filter. The values are the names
        /// of query parameters defined by this filter.
        /// </summary>
        /// <remarks>
        /// The keys of the pairs in the result of this method map to the keys of the pairs returned by
        /// the <see cref="Values"/> property.
        /// </remarks>
        public IEnumerable<string> QueryParameters
        {
            get
            {
                yield return "visibility";
                yield return "member_status";
                yield return "owner";
            }
        }

        /// <summary>
        /// Gets a collection of filter values defined by this filter. The keys of the pairs in the result
        /// of this method are the query parameter names, and the values are the actual filter values
        /// defined for that parameter.
        /// </summary>
        /// <remarks>
        /// Filter values which are not set (i.e. have a <see langword="null"/> value) are omitted from the
        /// result.
        /// </remarks>
        public IEnumerable<KeyValuePair<string, string>> Values
        {
            get
            {
                if (Visibility != null)
                    yield return new KeyValuePair<string, string>("visibility", Visibility.Name);
                if (MemberStatus != null)
                    yield return new KeyValuePair<string, string>("member_status", MemberStatus.Name);
                if (Owner != null)
                    yield return new KeyValuePair<string, string>("owner", Owner.Value);
            }
        }
    }
}
