namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using System;
    using System.Collections.Concurrent;
    using net.openstack.Core;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents an image member status.
    /// </summary>
    /// <remarks>
    /// This class functions as a strongly-typed enumeration of known member statuses,
    /// with added support for unknown statuses returned by a server extension.
    /// </remarks>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonConverter(typeof(MemberStatus.Converter))]
    public sealed class MemberStatus : ExtensibleEnum<MemberStatus>
    {
        private static readonly ConcurrentDictionary<string, MemberStatus> _states =
            new ConcurrentDictionary<string, MemberStatus>(StringComparer.OrdinalIgnoreCase);
        private static readonly MemberStatus _pending = FromName("pending");
        private static readonly MemberStatus _accepted = FromName("accepted");
        private static readonly MemberStatus _rejected = FromName("rejected");
        private static readonly MemberStatus _all = FromName("all");

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberStatus"/> class with the specified name.
        /// </summary>
        /// <inheritdoc/>
        private MemberStatus(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Gets the <see cref="MemberStatus"/> instance with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="name"/> is empty.</exception>
        public static MemberStatus FromName(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be empty");

            return _states.GetOrAdd(name, i => new MemberStatus(i));
        }

        /// <summary>
        /// Gets a <see cref="MemberStatus"/> instance representing the initial state of an image member.
        /// The image is not visible in the member's image list, but the member can still create instances
        /// from the image by <see cref="ImageMember.ImageId"/>.
        /// </summary>
        public static MemberStatus Pending
        {
            get
            {
                return _pending;
            }
        }

        /// <summary>
        /// Gets a <see cref="MemberStatus"/> instance representing an image member which has been accepted.
        /// The image is visible in the member's image list.
        /// </summary>
        public static MemberStatus Accepted
        {
            get
            {
                return _accepted;
            }
        }

        /// <summary>
        /// Gets a <see cref="MemberStatus"/> instance representing an image member which has been rejected.
        /// The image is not visible in the member's image list, but the member can still create instances
        /// from the image by <see cref="ImageMember.ImageId"/>.
        /// </summary>
        /// <remarks>
        /// This status is equivalent to <see cref="Pending"/>, but it signifies that the member has explicitly
        /// set the state.
        /// </remarks>
        public static MemberStatus Rejected
        {
            get
            {
                return _rejected;
            }
        }

        /// <summary>
        /// Gets a <see cref="MemberStatus"/> instance which, when used in an <see cref="ImageFilter"/>,
        /// indicates that an image listing should not be filtered according to the <see cref="ImageMember.Status"/>
        /// property.
        /// </summary>
        public static MemberStatus All
        {
            get
            {
                return _all;
            }
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="MemberStatus"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override MemberStatus FromName(string name)
            {
                return MemberStatus.FromName(name);
            }
        }
    }
}
