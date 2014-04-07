﻿namespace Rackspace.Services.Databases.V1
{
    using System;
    using Newtonsoft.Json;
    using OpenStack.Services.Databases.V1;
    using ExtensibleJsonObject = OpenStack.ObjectModel.ExtensibleJsonObject;

    /// <summary>
    /// This class models the JSON representation of a restore point, used to restore
    /// a database instance from a backup during a call to
    /// <see cref="IDatabaseService.CreateDatabaseInstanceAsync"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class RestorePoint : ExtensibleJsonObject
    {
        [JsonProperty("backupRef", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private BackupId _backupId;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestorePoint"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected RestorePoint()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestorePoint"/> class
        /// with the specified backup ID.
        /// </summary>
        /// <param name="backupId">The backup ID. This is obtained from <see cref="Backup.Id">Backup.Id</see>.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="backupId"/> is <see langword="null"/>.</exception>
        public RestorePoint(BackupId backupId)
        {
            if (backupId == null)
                throw new ArgumentNullException("backupId");

            _backupId = backupId;
        }

        /// <summary>
        /// Gets the unique identifier for this backup.
        /// </summary>
        public BackupId BackupId
        {
            get
            {
                return _backupId;
            }
        }
    }
}
