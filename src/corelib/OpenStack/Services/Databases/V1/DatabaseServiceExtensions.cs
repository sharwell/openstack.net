namespace OpenStack.Services.Databases.V1
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using OpenStack.Collections;
    using OpenStack.Threading;
    using Rackspace.Threading;

    public static class DatabaseServiceExtensions
    {
        #region Database instances

        public static Task<DatabaseInstance> CreateDatabaseInstanceAsync(this IDatabaseService service, DatabaseInstanceData instanceData, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<DatabaseInstance> progress)
        {
            if (service == null)
                throw new ArgumentNullException("service");
            if (completionOption != AsyncCompletionOption.RequestSubmitted)
                throw new NotImplementedException();

            return
                CoreTaskExtensions.Using(
                    () => service.PrepareCreateDatabaseInstanceAsync(new DatabaseInstanceRequest(instanceData), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Instance);
        }

        public static Task<ReadOnlyCollectionPage<DatabaseInstance>> ListDatabaseInstancesAsync(this IDatabaseService service, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareListDatabaseInstancesAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<DatabaseInstance> GetDatabaseInstanceAsync(this IDatabaseService service, DatabaseInstanceId instanceId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetDatabaseInstanceAsync(instanceId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Instance);
        }

        public static Task RemoveDatabaseInstanceAsync(this IDatabaseService service, DatabaseInstanceId instanceId, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<DatabaseInstance> progress)
        {
            if (service == null)
                throw new ArgumentNullException("service");
            if (completionOption != AsyncCompletionOption.RequestSubmitted)
                throw new NotImplementedException();

            return CoreTaskExtensions.Using(
                () => service.PrepareRemoveDatabaseInstanceAsync(instanceId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task<DatabaseUser> EnableRootUserAsync(this IDatabaseService service, DatabaseInstanceId instanceId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareEnableRootUserAsync(instanceId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.User);
        }

        public static Task<bool?> CheckRootEnabledAsync(this IDatabaseService service, DatabaseInstanceId instanceId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareCheckRootEnabledAsync(instanceId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.RootEnabled);
        }

        #endregion

        #region Database instance actions

        public static Task RestartDatabaseInstanceAsync(this IDatabaseService service, DatabaseInstanceId instanceId, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<DatabaseInstance> progress)
        {
            if (service == null)
                throw new ArgumentNullException("service");
            if (completionOption != AsyncCompletionOption.RequestSubmitted)
                throw new NotImplementedException();

            return CoreTaskExtensions.Using(
                () => service.PrepareRestartDatabaseInstanceAsync(instanceId, new RestartRequest(new RestartData()), cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task ResizeDatabaseInstanceAsync(this IDatabaseService service, DatabaseInstanceId instanceId, FlavorRef flavor, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<DatabaseInstance> progress)
        {
            if (service == null)
                throw new ArgumentNullException("service");
            if (completionOption != AsyncCompletionOption.RequestSubmitted)
                throw new NotImplementedException();

            return CoreTaskExtensions.Using(
                () => service.PrepareResizeDatabaseInstanceAsync(instanceId, new ResizeRequest(new ResizeData(flavor)), cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task ResizeDatabaseInstanceVolumeAsync(this IDatabaseService service, DatabaseInstanceId instanceId, int? size, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<DatabaseInstance> progress)
        {
            if (service == null)
                throw new ArgumentNullException("service");
            if (completionOption != AsyncCompletionOption.RequestSubmitted)
                throw new NotImplementedException();

            return CoreTaskExtensions.Using(
                () => service.PrepareResizeDatabaseInstanceVolumeAsync(instanceId, new ResizeVolumeRequest(new ResizeVolumeData(new ResizeVolumeData.VolumeData(size))), cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        #endregion

        #region Databases

        public static Task CreateDatabasesAsync(this IDatabaseService service, DatabaseInstanceId instanceId, IEnumerable<DatabaseData> databases, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareCreateDatabasesAsync(instanceId, new DatabasesRequest(databases), cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task<ReadOnlyCollectionPage<Database>> ListDatabasesAsync(this IDatabaseService service, DatabaseInstanceId instanceId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareListDatabasesAsync(instanceId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task RemoveDatabaseAsync(this IDatabaseService service, DatabaseInstanceId instanceId, DatabaseName databaseName, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareRemoveDatabaseAsync(instanceId, databaseName, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        #endregion

        #region Users

        public static Task CreateUsersAsync(this IDatabaseService service, DatabaseInstanceId instanceId, IEnumerable<DatabaseUserData> users, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareCreateUsersAsync(instanceId, new UsersRequest(users), cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task<ReadOnlyCollectionPage<DatabaseUser>> ListUsersAsync(this IDatabaseService service, DatabaseInstanceId instanceId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareListUsersAsync(instanceId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task RemoveUserAsync(this IDatabaseService service, DatabaseInstanceId instanceId, UserName userName, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareRemoveUserAsync(instanceId, userName, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        #endregion

        #region Flavors

        public static Task<ReadOnlyCollectionPage<DatabaseFlavor>> ListFlavorsAsync(this IDatabaseService service, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareListFlavorsAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<DatabaseFlavor> GetFlavorAsync(this IDatabaseService service, FlavorId flavorId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetFlavorAsync(flavorId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Flavor);
        }

        #endregion
    }
}
