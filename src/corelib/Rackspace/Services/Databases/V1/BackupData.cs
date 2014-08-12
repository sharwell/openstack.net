namespace Rackspace.Services.Databases.V1
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.Services.Databases.V1;
    using ExtensibleJsonObject = OpenStack.ObjectModel.ExtensibleJsonObject;

    /// <summary>
    /// This class models the JSON representation of a database instance backup configuration in the <see cref="IDatabaseService"/>.
    /// </summary>
    /// <seealso cref="IDatabaseBackupExtension.CreateBackupAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class BackupData : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="InstanceId"/> property.
        /// </summary>
        [JsonProperty("instance", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private DatabaseInstanceId _instanceId;

        /// <summary>
        /// This is the backing field for the <see cref="Name"/> property.
        /// </summary>
        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _name;

        /// <summary>
        /// This is the backing field for the <see cref="Description"/> property.
        /// </summary>
        [JsonProperty("description", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _description;

        /// <summary>
        /// Initializes a new instance of the <see cref="BackupData"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected BackupData()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BackupData"/> class with
        /// the specified values.
        /// </summary>
        /// <param name="instanceId">The database instance ID.</param>
        /// <param name="name">The name of the backup.</param>
        /// <param name="description">The optional description of the backup.</param>
        public BackupData(DatabaseInstanceId instanceId, string name, string description)
        {
            Initialize(instanceId, name, description);
        }

        public BackupData(DatabaseInstanceId instanceId, string name, string description, params JProperty[] extensionData)
            : base(extensionData)
        {
            Initialize(instanceId, name, description);
        }

        public BackupData(DatabaseInstanceId instanceId, string name, string description, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            Initialize(instanceId, name, description);
        }

        /// <summary>
        /// Gets the identifier of the database instance which should be backed up.
        /// </summary>
        public virtual DatabaseInstanceId InstanceId
        {
            get
            {
                return _instanceId;
            }
        }

        /// <summary>
        /// Gets the name of this backup.
        /// </summary>
        public virtual string Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Gets the description of this backup.
        /// </summary>
        public virtual string Description
        {
            get
            {
                return _description;
            }
        }

        private void Initialize(DatabaseInstanceId instanceId, string name, string description)
        {
            _instanceId = instanceId;
            _name = name;
            _description = description;
        }
    }
}
