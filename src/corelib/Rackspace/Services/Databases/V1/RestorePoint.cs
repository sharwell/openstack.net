namespace Rackspace.Services.Databases.V1
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
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
        /// <param name="backupId">The backup ID.</param>
        public RestorePoint(BackupId backupId)
        {
            _backupId = backupId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestorePoint"/> class
        /// with the specified backup ID.
        /// </summary>
        /// <param name="backupId">The backup ID.</param>
        public RestorePoint(BackupId backupId, params JProperty[] extensionData)
            : base(extensionData)
        {
            _backupId = backupId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestorePoint"/> class
        /// with the specified backup ID.
        /// </summary>
        /// <param name="backupId">The backup ID.</param>
        public RestorePoint(BackupId backupId, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
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
