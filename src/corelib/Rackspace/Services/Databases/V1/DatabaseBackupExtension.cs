namespace Rackspace.Services.Databases.V1
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;
    using OpenStack.Collections;
    using OpenStack.Net;
    using OpenStack.Services.Databases.V1;
    using OpenStack.Threading;
    using Rackspace.Threading;

    public static class DatabaseBackupExtension
    {
        private const string RestorePointProperty = "restorePoint";

        #region Backups

        /// <summary>
        /// Prepare an API call to create a backup of a database instance.
        /// </summary>
        /// <param name="service">The <see cref="IDatabaseService"/> instance.</param>
        /// <param name="backup">A <see cref="BackupData"/> instance providing the parameters for the backup.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the prepared API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="backup"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the preparation of this API call.</exception>
        /// <seealso cref="CreateBackupApiCall"/>
        /// <seealso cref="DatabaseBackupExtension.CreateBackupAsync"/>
        /// <seealso href="http://docs.rackspace.com/cdb/api/v1.0/cdb-devguide/content/POST_createBackup__version___accountId__backups_.html">Create Backup (Rackspace Cloud Databases Developer Guide - API v1.0)</seealso>
        public static extern Task<CreateBackupApiCall> PrepareCreateBackupAsync(this IDatabaseService service, BackupData backup, CancellationToken cancellationToken);

        /// <summary>
        /// Prepare an API call to get a collection of all backups for database instances in an account.
        /// </summary>
        /// <param name="service">The <see cref="IDatabaseService"/> instance.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the prepared API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="service"/> is <see langword="null"/>.</exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the preparation of this API call.</exception>
        /// <seealso cref="ListBackupsApiCall"/>
        /// <seealso cref="DatabaseBackupExtension.ListBackupsAsync(IDatabaseService, CancellationToken)"/>
        /// <seealso href="http://docs.rackspace.com/cdb/api/v1.0/cdb-devguide/content/GET_getBackups__version___accountId__backups_.html">List Backups (Rackspace Cloud Databases Developer Guide - API v1.0)</seealso>
        public static extern Task<ListBackupsApiCall> PrepareListBackupsAsync(this IDatabaseService service, CancellationToken cancellationToken);

        /// <summary>
        /// Prepare an API call to get information about a database instance backup by ID.
        /// </summary>
        /// <param name="service">The <see cref="IDatabaseService"/> instance.</param>
        /// <param name="backupId">The backup ID.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the prepared API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="backupId"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the preparation of this API call.</exception>
        /// <seealso cref="GetBackupApiCall"/>
        /// <seealso cref="DatabaseBackupExtension.GetBackupAsync"/>
        /// <seealso href="http://docs.rackspace.com/cdb/api/v1.0/cdb-devguide/content/GET_getBackupById__version___accountId__backups__backupId__.html">List Backup by ID (Rackspace Cloud Databases Developer Guide - API v1.0)</seealso>
        public static extern Task<GetBackupApiCall> PrepareGetBackupAsync(this IDatabaseService service, BackupId backupId, CancellationToken cancellationToken);

        /// <summary>
        /// Prepare an API call to remove a database instance backup.
        /// </summary>
        /// <param name="service">The <see cref="IDatabaseService"/> instance.</param>
        /// <param name="backupId">The backup ID.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the prepared API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="backupId"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the preparation of this API call.</exception>
        /// <seealso cref="RemoveBackupApiCall"/>
        /// <seealso cref="DatabaseBackupExtension.RemoveBackupAsync"/>
        /// <seealso href="http://docs.rackspace.com/cdb/api/v1.0/cdb-devguide/content/DELETE_deleteBackup__version___accountId__backups__backupId__.html">Delete Backup (Rackspace Cloud Databases Developer Guide - API v1.0)</seealso>
        public static extern Task<RemoveBackupApiCall> PrepareRemoveBackupAsync(this IDatabaseService service, BackupId backupId, CancellationToken cancellationToken);

        /// <summary>
        /// Prepare an API call to get a collection of all backups for a particular database instance.
        /// </summary>
        /// <param name="service">The <see cref="IDatabaseService"/> instance.</param>
        /// <param name="instanceId">The database instance ID.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the prepared API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="instanceId"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the preparation of this API call.</exception>
        /// <seealso cref="ListBackupsApiCall"/>
        /// <seealso cref="DatabaseBackupExtension.ListBackupsAsync(IDatabaseService, DatabaseInstanceId, CancellationToken)"/>
        /// <seealso href="http://docs.rackspace.com/cdb/api/v1.0/cdb-devguide/content/GET_getBackups__version___accountId__backups_.html">List Backups (Rackspace Cloud Databases Developer Guide - API v1.0)</seealso>
        public static extern Task<ListBackupsApiCall> PrepareListBackupsAsync(this IDatabaseService service, DatabaseInstanceId instanceId, CancellationToken cancellationToken);

        #endregion

        public static Task<Backup> CreateBackupAsync(this IDatabaseService service, BackupData configuration, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<DatabaseInstance> progress)
        {
            if (service == null)
                throw new ArgumentNullException("service");
            if (completionOption != AsyncCompletionOption.RequestSubmitted)
                throw new NotImplementedException();

            return
                CoreTaskExtensions.Using(
                    () => service.PrepareCreateBackupAsync(configuration, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Backup);
        }

        public static Task<ReadOnlyCollectionPage<Backup>> ListBackupsAsync(this IDatabaseService service, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareListBackupsAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<Backup> GetBackupAsync(this IDatabaseService service, BackupId backupId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetBackupAsync(backupId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Backup);
        }

        public static Task RemoveBackupAsync(this IDatabaseService service, BackupId backupId, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareRemoveBackupAsync(backupId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task<ReadOnlyCollectionPage<Backup>> ListBackupsAsync(this IDatabaseService service, DatabaseInstanceId instanceId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareListBackupsAsync(instanceId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static RestorePoint GetRestorePoint(this DatabaseInstanceData instanceConfiguration)
        {
            if (instanceConfiguration == null)
                throw new ArgumentNullException("instanceConfiguration");

            JToken value;
            if (!instanceConfiguration.ExtensionData.TryGetValue(RestorePointProperty, out value) || value == null)
                return null;

            return value.ToObject<RestorePoint>();
        }

        public static DatabaseInstanceData WithRestorePoint(this DatabaseInstanceData instanceConfiguration, RestorePoint restorePoint)
        {
            if (instanceConfiguration == null)
                throw new ArgumentNullException("instanceConfiguration");
            if (restorePoint == null)
                throw new ArgumentNullException("restorePoint");

            // TODO: should this remove the restorePoint property if restorePoint==null?
            Dictionary<string, JToken> extensionData = new Dictionary<string, JToken>(instanceConfiguration.ExtensionData);
            extensionData[RestorePointProperty] = JToken.FromObject(restorePoint);
            return new DatabaseInstanceData(instanceConfiguration.FlavorRef, instanceConfiguration.Volume, instanceConfiguration.Name, extensionData);
        }
    }
}
