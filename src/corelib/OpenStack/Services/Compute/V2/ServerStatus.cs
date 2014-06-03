namespace OpenStack.Services.Compute.V2
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// Represents the state of a compute server.
    /// </summary>
    /// <remarks>
    /// This class functions as a strongly-typed enumeration of known server statuses,
    /// with added support for unknown statuses returned by a server extension.
    /// </remarks>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonConverter(typeof(ServerStatus.Converter))]
    public sealed class ServerStatus : ExtensibleEnum<ServerStatus>
    {
        private static readonly ConcurrentDictionary<string, ServerStatus> _states =
            new ConcurrentDictionary<string, ServerStatus>(StringComparer.OrdinalIgnoreCase);
        private static readonly ServerStatus _active = FromName("ACTIVE");
        private static readonly ServerStatus _build = FromName("BUILD");
        private static readonly ServerStatus _deleted = FromName("DELETED");
        private static readonly ServerStatus _error = FromName("ERROR");
        private static readonly ServerStatus _hardReboot = FromName("HARD_REBOOT");
        private static readonly ServerStatus _migrating = FromName("MIGRATING");
        private static readonly ServerStatus _password = FromName("PASSWORD");
        private static readonly ServerStatus _reboot = FromName("REBOOT");
        private static readonly ServerStatus _rebuild = FromName("REBUILD");
        private static readonly ServerStatus _resize = FromName("RESIZE");
        private static readonly ServerStatus _revertResize = FromName("REVERT_RESIZE");
        private static readonly ServerStatus _suspended = FromName("SUSPENDED");
        private static readonly ServerStatus _unknown = FromName("UNKNOWN");
        private static readonly ServerStatus _verifyResize = FromName("VERIFY_RESIZE");

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerStatus"/> class with the specified name.
        /// </summary>
        /// <inheritdoc/>
        private ServerStatus(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Gets the <see cref="ServerStatus"/> instance with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The unique <see cref="ServerStatus"/> instance with the specified name.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="name"/> is empty.</exception>
        public static ServerStatus FromName(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be empty");

            return _states.GetOrAdd(name, i => new ServerStatus(i));
        }

        /// <summary>
        /// Gets a <see cref="ServerStatus"/> representing a server which is active and ready to use.
        /// </summary>
        public static ServerStatus Active
        {
            get
            {
                return _active;
            }
        }

        /// <summary>
        /// Gets a <see cref="ServerStatus"/> representing a server which is currently being built.
        /// </summary>
        public static ServerStatus Build
        {
            get
            {
                return _build;
            }
        }

        /// <summary>
        /// Gets a <see cref="ServerStatus"/> representing a server which has been deleted.
        /// </summary>
        /// <remarks>
        /// By default, the <see cref="IComputeService.PrepareListServersAsync"/> API does not
        /// return servers which have been deleted. To list deleted servers, use the
        /// <see cref="ComputeServiceExtensions.WithChangesSince(Task{ListServersApiCall}, DateTimeOffset)"/>
        /// method to modify the API call by adding the <c>changes-since</c> parameter.
        /// </remarks>
        public static ServerStatus Deleted
        {
            get
            {
                return _deleted;
            }
        }

        /// <summary>
        /// Gets a <see cref="ServerStatus"/> representing a server which failed to perform
        /// an operation and is now in an error state.
        /// </summary>
        public static ServerStatus Error
        {
            get
            {
                return _error;
            }
        }

        /// <summary>
        /// Gets a <see cref="ServerStatus"/> representing a server currently performing a hard
        /// reboot. When the reboot operation completes, the server will be in the <see cref="Active"/>
        /// state.
        /// </summary>
        /// <seealso cref="IComputeService.PrepareRebootServerAsync"/>
        public static ServerStatus HardReboot
        {
            get
            {
                return _hardReboot;
            }
        }

        /// <summary>
        /// Gets a <see cref="ServerStatus"/> representing a server which is currently being moved
        /// from one physical node to another.
        /// </summary>
        /// <remarks>
        /// <note>Server migration is a Rackspace-specific extension.</note>
        /// </remarks>
        public static ServerStatus Migrating
        {
            get
            {
                return _migrating;
            }
        }

        /// <summary>
        /// Gets a <see cref="ServerStatus"/> representing the password for the server is being changed.
        /// </summary>
        /// <seealso cref="IComputeService.PrepareChangePasswordAsync"/>
        public static ServerStatus Password
        {
            get
            {
                return _password;
            }
        }

        /// <summary>
        /// Gets a <see cref="ServerStatus"/> representing a server currently performing a soft
        /// reboot. When the reboot operation completes, the server will be in the <see cref="Active"/>
        /// state.
        /// </summary>
        /// <seealso cref="IComputeService.PrepareRebootServerAsync"/>
        public static ServerStatus Reboot
        {
            get
            {
                return _reboot;
            }
        }

        /// <summary>
        /// Gets a <see cref="ServerStatus"/> representing a server currently being rebuilt.
        /// When the rebuild operation completes, the server will be in the <see cref="Active"/>
        /// state if the rebuild was successful; otherwise, it will be in the <see cref="Error"/> state
        /// if the rebuild operation failed.
        /// </summary>
        /// <seealso cref="IComputeService.PrepareRebuildServerAsync"/>
        public static ServerStatus Rebuild
        {
            get
            {
                return _rebuild;
            }
        }

        /// <summary>
        /// Gets a <see cref="ServerStatus"/> representing a server currently executing a resize action.
        /// When the resize operation completes, the server will be in the <see cref="VerifyResize"/>
        /// state if the resize was successful; otherwise, it will be in the <see cref="Active"/> state
        /// if the resize operation failed.
        /// </summary>
        /// <seealso cref="IComputeService.PrepareResizeServerAsync"/>
        public static ServerStatus Resize
        {
            get
            {
                return _resize;
            }
        }

        /// <summary>
        /// Gets a <see cref="ServerStatus"/> representing a server currently executing a revert resize action.
        /// </summary>
        /// <seealso cref="IComputeService.PrepareRevertResizedServerAsync"/>
        public static ServerStatus RevertResize
        {
            get
            {
                return _revertResize;
            }
        }

        /// <summary>
        /// Gets a <see cref="ServerStatus"/> for a server that is currently inactive, either by request or necessity.
        /// </summary>
        public static ServerStatus Suspended
        {
            get
            {
                return _suspended;
            }
        }

        /// <summary>
        /// Gets a <see cref="ServerStatus"/> for a server that is currently in an unknown state.
        /// </summary>
        public static ServerStatus Unknown
        {
            get
            {
                return _unknown;
            }
        }

        /// <summary>
        /// Gets a <see cref="ServerStatus"/> representing a server which completed a resize operation
        /// and is now waiting for the operation to be confirmed or reverted.
        /// </summary>
        /// <seealso cref="IComputeService.PrepareConfirmServerResizedAsync"/>
        public static ServerStatus VerifyResize
        {
            get
            {
                return _verifyResize;
            }
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="ServerStatus"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override ServerStatus FromName(string name)
            {
                return ServerStatus.FromName(name);
            }
        }
    }
}
