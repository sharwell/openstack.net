namespace Rackspace.Services.Databases.V1
{
    using System;
    using Newtonsoft.Json;
    using OpenStack.Services.Databases.V1;

    /// <summary>
    /// This class models the JSON object used to describe a new database instance in the <see cref="IDatabaseService"/>.
    /// </summary>
    /// <seealso cref="IDatabaseService.CreateDatabaseInstanceAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class RestoreDatabaseInstanceConfiguration : DatabaseInstanceConfiguration
    {
        /// <summary>
        /// This is the backing field for the <see cref="RestorePoint"/> property.
        /// </summary>
        [JsonProperty("restorePoint", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private RestorePoint _restorePoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestoreDatabaseInstanceConfiguration"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected RestoreDatabaseInstanceConfiguration()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestoreDatabaseInstanceConfiguration"/> class
        /// with a restore point for restoring a database instance from a backup.
        /// </summary>
        /// <param name="flavorRef">A <see cref="FlavorRef"/> object describing the flavor of the database instance.</param>
        /// <param name="volumeConfiguration">A <see cref="DatabaseVolumeConfiguration"/> object containing additional information about
        /// the database instance storage volume.</param>
        /// <param name="name">The name of the database instance, or <see langword="null"/> if the database instance is not named.</param>
        /// <param name="restorePoint">A <see cref="RestorePoint"/> object describing the backup from which this database instance was restored, or null if the restore point is not available.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="flavorRef"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="volumeConfiguration"/> is <see langword="null"/>.</para>
        /// </exception>
        public RestoreDatabaseInstanceConfiguration(FlavorRef flavorRef, DatabaseVolumeConfiguration volumeConfiguration, string name, RestorePoint restorePoint)
            : base(flavorRef, volumeConfiguration, name)
        {
            _restorePoint = restorePoint;
        }

        /// <summary>
        /// Gets a <see cref="RestorePoint"/> object describing the backup from which this database instance was restored.
        /// </summary>
        public RestorePoint RestorePoint
        {
            get
            {
                return _restorePoint;
            }
        }
    }
}
