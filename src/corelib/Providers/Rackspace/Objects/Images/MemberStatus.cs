namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using System;
    using System.Collections.Concurrent;
    using net.openstack.Core;
    using Newtonsoft.Json;

    [JsonConverter(typeof(MemberStatus.Converter))]
    public sealed class MemberStatus : ExtensibleEnum<MemberStatus>
    {
        private static readonly ConcurrentDictionary<string, MemberStatus> _states =
            new ConcurrentDictionary<string, MemberStatus>(StringComparer.OrdinalIgnoreCase);
        private static readonly MemberStatus _pending = FromName("pending");
        private static readonly MemberStatus _accepted = FromName("accepted");
        private static readonly MemberStatus _rejected = FromName("rejected");

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
        /// Gets a <see cref="MemberStatus"/> instance representing <placeholder>description</placeholder>.
        /// </summary>
        public static MemberStatus Pending
        {
            get
            {
                return _pending;
            }
        }

        /// <summary>
        /// Gets a <see cref="MemberStatus"/> instance representing <placeholder>description</placeholder>.
        /// </summary>
        public static MemberStatus Accepted
        {
            get
            {
                return _accepted;
            }
        }

        /// <summary>
        /// Gets a <see cref="MemberStatus"/> instance representing <placeholder>description</placeholder>.
        /// </summary>
        public static MemberStatus Rejected
        {
            get
            {
                return _rejected;
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
