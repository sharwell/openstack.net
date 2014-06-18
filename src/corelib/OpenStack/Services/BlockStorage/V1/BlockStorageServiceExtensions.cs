namespace OpenStack.Services.BlockStorage.V1
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.Collections;
    using OpenStack.Threading;
    using Rackspace.Threading;
    using Encoding = System.Text.Encoding;

    public static class BlockStorageServiceExtensions
    {
        #region Volumes

        public static Task<Volume> CreateVolumeAsync(this IBlockStorageService service, VolumeData volume, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<Volume> progress)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            if (completionOption != AsyncCompletionOption.RequestSubmitted)
                throw new NotImplementedException();

            return
                CoreTaskExtensions.Using(
                    () => service.PrepareCreateVolumeAsync(new VolumeRequest(volume), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Volume);
        }

        public static Task<ReadOnlyCollectionPage<Volume>> ListVolumesAsync(this IBlockStorageService service, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return
                CoreTaskExtensions.Using(
                    () => service.PrepareListVolumesAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<Volume> GetVolumeAsync(this IBlockStorageService service, VolumeId volumeId, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetVolumeAsync(volumeId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Volume);
        }

        public static Task RemoveVolumeAsync(this IBlockStorageService service, VolumeId volumeId, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<Volume> progress)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            if (completionOption != AsyncCompletionOption.RequestSubmitted)
                throw new NotImplementedException();

            return CoreTaskExtensions.Using(
                () => service.PrepareRemoveVolumeAsync(volumeId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        #endregion

        #region Snapshots

        public static Task<Snapshot> CreateSnapshotAsync(this IBlockStorageService service, SnapshotData snapshot, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<Snapshot> progress)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            if (completionOption != AsyncCompletionOption.RequestSubmitted)
                throw new NotImplementedException();

            return
                CoreTaskExtensions.Using(
                    () => service.PrepareCreateSnapshotAsync(new SnapshotRequest(snapshot), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Snapshot);
        }

        public static Task<ReadOnlyCollectionPage<Snapshot>> ListSnapshotsAsync(this IBlockStorageService service, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return
                CoreTaskExtensions.Using(
                    () => service.PrepareListSnapshotsAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<Snapshot> GetSnapshotAsync(this IBlockStorageService service, SnapshotId snapshotId, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetSnapshotAsync(snapshotId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Snapshot);
        }

        public static Task RemoveSnapshotAsync(this IBlockStorageService service, SnapshotId snapshotId, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<Snapshot> progress)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            if (completionOption != AsyncCompletionOption.RequestSubmitted)
                throw new NotImplementedException();

            return CoreTaskExtensions.Using(
                () => service.PrepareRemoveSnapshotAsync(snapshotId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        #endregion

        #region Volume Types

        public static Task<ReadOnlyCollectionPage<VolumeType>> ListVolumeTypesAsync(this IBlockStorageService service, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return
                CoreTaskExtensions.Using(
                    () => service.PrepareListVolumeTypesAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<VolumeType> GetVolumeTypeAsync(this IBlockStorageService service, VolumeTypeId volumeTypeId, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetVolumeTypeAsync(volumeTypeId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.VolumeType);
        }

        #endregion

        #region Optional Parameters

        public static Task<CreateSnapshotApiCall> WithForce(this Task<CreateSnapshotApiCall> task)
        {
            if (task == null)
                throw new ArgumentNullException("task");

            return task
                .Then(_ => task.Result.RequestMessage.Content.ReadAsStringAsync())
                .Select(
                    innerTask =>
                    {
                        JObject body = JObject.Parse(innerTask.Result);
                        JObject snapshot = body["snapshot"] as JObject;
                        if (snapshot != null)
                            snapshot["force"] = JToken.FromObject(true);

                        CreateSnapshotApiCall apiCall = task.Result;
                        apiCall.RequestMessage.Content = new StringContent(body.ToString(Formatting.None), Encoding.UTF8);
                        return apiCall;
                    });
        }

        #endregion
    }
}
