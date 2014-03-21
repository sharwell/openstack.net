﻿namespace net.openstack.Providers.Rackspace.Objects.Images
{
    using System;
    using System.Collections.Concurrent;
    using net.openstack.Core;
    using Newtonsoft.Json;

    [JsonConverter(typeof(ImageStatus.Converter))]
    public sealed class ImageStatus : ExtensibleEnum<ImageStatus>
    {
        private static readonly ConcurrentDictionary<string, ImageStatus> _states =
            new ConcurrentDictionary<string, ImageStatus>(StringComparer.OrdinalIgnoreCase);
        private static readonly ImageStatus _queued = FromName("queued");
        private static readonly ImageStatus _saving = FromName("saving");
        private static readonly ImageStatus _active = FromName("active");
        private static readonly ImageStatus _killed = FromName("killed");
        private static readonly ImageStatus _deleted = FromName("deleted");
        private static readonly ImageStatus _pendingDelete = FromName("pending_delete");

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageStatus"/> class with the specified name.
        /// </summary>
        /// <inheritdoc/>
        private ImageStatus(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Gets the <see cref="ImageStatus"/> instance with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="name"/> is empty.</exception>
        public static ImageStatus FromName(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be empty");

            return _states.GetOrAdd(name, i => new ImageStatus(i));
        }

        /// <summary>
        /// Gets a <see cref="ImageStatus"/> instance representing <placeholder>description</placeholder>.
        /// </summary>
        public static ImageStatus Queued
        {
            get
            {
                return _queued;
            }
        }

        /// <summary>
        /// Gets a <see cref="ImageStatus"/> instance representing <placeholder>description</placeholder>.
        /// </summary>
        public static ImageStatus Saving
        {
            get
            {
                return _saving;
            }
        }

        /// <summary>
        /// Gets a <see cref="ImageStatus"/> instance representing <placeholder>description</placeholder>.
        /// </summary>
        public static ImageStatus Active
        {
            get
            {
                return _active;
            }
        }

        /// <summary>
        /// Gets a <see cref="ImageStatus"/> instance representing <placeholder>description</placeholder>.
        /// </summary>
        public static ImageStatus Killed
        {
            get
            {
                return _killed;
            }
        }

        /// <summary>
        /// Gets a <see cref="ImageStatus"/> instance representing <placeholder>description</placeholder>.
        /// </summary>
        public static ImageStatus Deleted
        {
            get
            {
                return _deleted;
            }
        }

        /// <summary>
        /// Gets a <see cref="ImageStatus"/> instance representing <placeholder>description</placeholder>.
        /// </summary>
        public static ImageStatus PendingDelete
        {
            get
            {
                return _pendingDelete;
            }
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="ImageStatus"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override ImageStatus FromName(string name)
            {
                return ImageStatus.FromName(name);
            }
        }
    }
}