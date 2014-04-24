namespace Rackspace.Services.Databases.V1
{
    using System;
    using OpenStack.Services.Databases.V1;

    public static class DatabaseBackupExtension
    {
        public static bool TryGetBackupExtension(this IDatabaseService databaseService, out IDatabaseBackupExtension backupExtension)
        {
            if (databaseService == null)
                throw new ArgumentNullException("databaseService");

            backupExtension = databaseService as IDatabaseBackupExtension;
            return backupExtension != null;
        }

        public static RestorePoint GetRestorePoint(this DatabaseInstanceConfiguration instanceConfiguration)
        {
            RestoreDatabaseInstanceConfiguration restoreConfiguration = instanceConfiguration as RestoreDatabaseInstanceConfiguration;
            if (restoreConfiguration != null)
                return restoreConfiguration.RestorePoint;

            throw new NotImplementedException();
        }
    }
}
