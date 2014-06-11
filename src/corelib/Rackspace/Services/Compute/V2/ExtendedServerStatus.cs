namespace Rackspace.Services.Compute.V2
{
    using OpenStack.Services.Compute.V2;

    /// <summary>
    /// This class defines Rackspace-specific server status values which extend
    /// the default values defined by <see cref="ServerStatus"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public static class ExtendedServerStatus
    {
        private static readonly ServerStatus _rescue = ServerStatus.FromName("RESCUE");
        private static readonly ServerStatus _prepRescue = ServerStatus.FromName("PREP_RESCUE");
        private static readonly ServerStatus _prepUnrescue = ServerStatus.FromName("PREP_UNRESCUE");

        /// <summary>
        /// Gets a <see cref="ServerStatus"/> representing a server currently in rescue mode.
        /// </summary>
        /// <seealso cref="RescueExtensions.PrepareRescueServerAsync"/>
        /// <seealso cref="RescueExtensions.RescueServerAsync"/>
        public static ServerStatus Rescue
        {
            get
            {
                return _rescue;
            }
        }

        /// <summary>
        /// Gets a <see cref="ServerStatus"/> representing a server currently executing a rescue action.
        /// </summary>
        /// <seealso cref="RescueExtensions.PrepareRescueServerAsync"/>
        /// <seealso cref="RescueExtensions.RescueServerAsync"/>
        public static ServerStatus PrepRescue
        {
            get
            {
                return _prepRescue;
            }
        }

        /// <summary>
        /// Gets a <see cref="ServerStatus"/> representing a server currently executing an unrescue action.
        /// </summary>
        /// <seealso cref="RescueExtensions.PrepareUnrescueServerAsync"/>
        /// <seealso cref="RescueExtensions.UnrescueServerAsync"/>
        public static ServerStatus PrepUnrescue
        {
            get
            {
                return _prepUnrescue;
            }
        }
    }
}
