namespace Rackspace.Services.Databases.V1
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using OpenStack.Collections;
    using OpenStack.Services.Databases.V1;
    using OpenStack.Threading;
    using CancellationToken = System.Threading.CancellationToken;
    using WebException = System.Net.WebException;
#if NET35
    using global::Rackspace.Threading;
#endif

    /// <summary>
    /// Represents a provider for the Rackspace Cloud Databases service.
    /// </summary>
    /// <seealso href="http://docs.rackspace.com/cdb/api/v1.0/cdb-devguide/content/overview.html">Rackspace Cloud Databases Developer Guide - API v1.0</seealso>
    /// <preliminary/>
    public interface IDatabaseBackupExtension
    {
        #region Backups

        /// <summary>
        /// Create a backup of a database instance.
        /// </summary>
        /// <param name="configuration">A <see cref="BackupConfiguration"/> object containing the backup parameters.</param>
        /// <param name="completionOption">Specifies when the <see cref="Task"/> representing the asynchronous server operation should be considered complete.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <param name="progress">An optional callback object to receive progress notifications, if <paramref name="completionOption"/> is <see cref="AsyncCompletionOption.RequestCompleted"/>. If this is <see langword="null"/>, no progress notifications are sent.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully,
        /// the <see cref="Task{TResult}.Result"/> property will return a <see cref="Backup"/> object
        /// describing the backup. If <paramref name="completionOption"/> is
        /// <see cref="AsyncCompletionOption.RequestCompleted"/>, the task will not be considered complete until
        /// the database instance transitions out of the <see cref="DatabaseInstanceStatus.Backup"/> state.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="configuration"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="completionOption"/> is not a valid <see cref="AsyncCompletionOption"/>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cdb/api/v1.0/cdb-devguide/content/POST_createBackup__version___accountId__backups_.html">Create Backup (Rackspace Cloud Databases Developer Guide - API v1.0)</seealso>
        Task<Backup> CreateBackupAsync(BackupConfiguration configuration, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<DatabaseInstance> progress);

        /// <summary>
        /// Get a collection of all backups for database instances in an account.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully,
        /// the <see cref="Task{TResult}.Result"/> property will return a collection of <see cref="Backup"/> objects
        /// describing the database instance backups.
        /// </returns>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cdb/api/v1.0/cdb-devguide/content/GET_getBackups__version___accountId__backups_.html">List Backups (Rackspace Cloud Databases Developer Guide - API v1.0)</seealso>
        Task<ReadOnlyCollection<Backup>> ListBackupsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Get information about a database instance backup by ID.
        /// </summary>
        /// <param name="backupId">The backup ID. This is obtained from <see cref="Backup.Id">Backup.Id</see></param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully,
        /// the <see cref="Task{TResult}.Result"/> property will return a <see cref="Backup"/> object
        /// describing the backup.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="backupId"/> is <see langword="null"/>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cdb/api/v1.0/cdb-devguide/content/GET_getBackupById__version___accountId__backups__backupId__.html">List Backup by ID (Rackspace Cloud Databases Developer Guide - API v1.0)</seealso>
        Task<Backup> GetBackupAsync(BackupId backupId, CancellationToken cancellationToken);

        /// <summary>
        /// Remove and delete a database instance backup.
        /// </summary>
        /// <param name="backupId">The backup ID. This is obtained from <see cref="Backup.Id">Backup.Id</see></param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="backupId"/> is <see langword="null"/>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cdb/api/v1.0/cdb-devguide/content/DELETE_deleteBackup__version___accountId__backups__backupId__.html">Delete Backup (Rackspace Cloud Databases Developer Guide - API v1.0)</seealso>
        Task RemoveBackupAsync(BackupId backupId, CancellationToken cancellationToken);

        /// <summary>
        /// Get a collection of all backups for a particular database instance.
        /// </summary>
        /// <param name="instanceId">The database instance ID. This is obtained from <see cref="DatabaseInstance.Id">DatabaseInstance.Id</see>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully,
        /// the <see cref="Task{TResult}.Result"/> property will return a collection of <see cref="Backup"/> objects
        /// describing the backups for the database instance identified by <paramref name="instanceId"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="instanceId"/> is <see langword="null"/>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cdb/api/v1.0/cdb-devguide/content/GET_getBackups__version___accountId__backups_.html">List Backups (Rackspace Cloud Databases Developer Guide - API v1.0)</seealso>
        Task<ReadOnlyCollection<Backup>> ListBackupsForInstanceAsync(DatabaseInstanceId instanceId, CancellationToken cancellationToken);

        #endregion
    }
}
