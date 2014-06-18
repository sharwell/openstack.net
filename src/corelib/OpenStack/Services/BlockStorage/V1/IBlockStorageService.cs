namespace OpenStack.Services.BlockStorage.V1
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IBlockStorageService : IHttpService
    {
        #region Volumes

        Task<CreateVolumeApiCall> PrepareCreateVolumeAsync(VolumeRequest request, CancellationToken cancellationToken);

        Task<ListVolumesApiCall> PrepareListVolumesAsync(CancellationToken cancellationToken);

        Task<GetVolumeApiCall> PrepareGetVolumeAsync(VolumeId volumeId, CancellationToken cancellationToken);

        Task<RemoveVolumeApiCall> PrepareRemoveVolumeAsync(VolumeId volumeId, CancellationToken cancellationToken);

        #endregion

        #region Snapshots

        Task<CreateSnapshotApiCall> PrepareCreateSnapshotAsync(SnapshotRequest request, CancellationToken cancellationToken);

        Task<ListSnapshotsApiCall> PrepareListSnapshotsAsync(CancellationToken cancellationToken);

        Task<GetSnapshotApiCall> PrepareGetSnapshotAsync(SnapshotId snapshotId, CancellationToken cancellationToken);

        Task<RemoveSnapshotApiCall> PrepareRemoveSnapshotAsync(SnapshotId snapshotId, CancellationToken cancellationToken);

        #endregion

        #region Volume Types

        Task<ListVolumeTypesApiCall> PrepareListVolumeTypesAsync(CancellationToken cancellationToken);

        Task<GetVolumeTypeApiCall> PrepareGetVolumeTypeAsync(VolumeTypeId volumeTypeId, CancellationToken cancellationToken);

        #endregion
    }
}
