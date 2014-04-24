namespace Rackspace.Services.Databases.V1
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json.Linq;
    using OpenStack.Services.Databases.V1;

    public static class DatabaseBackupExtension
    {
        private const string RestorePointProperty = "restorePoint";

        public static bool TryGetBackupExtension(this IDatabaseService databaseService, out IDatabaseBackupExtension backupExtension)
        {
            if (databaseService == null)
                throw new ArgumentNullException("databaseService");

            backupExtension = databaseService as IDatabaseBackupExtension;
            return backupExtension != null;
        }

        public static RestorePoint GetRestorePoint(this DatabaseInstanceConfiguration instanceConfiguration)
        {
            if (instanceConfiguration == null)
                throw new ArgumentNullException("instanceConfiguration");

            JToken value;
            if (!instanceConfiguration.ExtensionData.TryGetValue(RestorePointProperty, out value) || value == null)
                return null;

            return value.ToObject<RestorePoint>();
        }

        public static DatabaseInstanceConfiguration WithRestorePoint(this DatabaseInstanceConfiguration instanceConfiguration, RestorePoint restorePoint)
        {
            if (instanceConfiguration == null)
                throw new ArgumentNullException("instanceConfiguration");
            if (restorePoint == null)
                throw new ArgumentNullException("restorePoint");

            // TODO: should this remove the restorePoint property if restorePoint==null?
            Dictionary<string, JToken> extensionData = new Dictionary<string, JToken>(instanceConfiguration.ExtensionData);
            extensionData[RestorePointProperty] = JToken.FromObject(restorePoint);
            return new DatabaseInstanceConfiguration(instanceConfiguration.FlavorRef, instanceConfiguration.Volume, instanceConfiguration.Name, extensionData);
        }
    }
}
