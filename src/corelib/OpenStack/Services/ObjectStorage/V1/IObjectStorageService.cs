namespace OpenStack.Services.ObjectStorage.V1
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Stream = System.IO.Stream;

#if !NET40PLUS
    using OpenStack.Compat;
    using Rackspace.Threading;
#endif

    public interface IObjectStorageService
    {
        #region Discoverability

        Task<GetObjectStorageInfoApiCall> PrepareGetObjectStorageInfoAsync(CancellationToken cancellationToken);

        #endregion

        #region Accounts

        Task<ListContainersApiCall> PrepareListContainersAsync(CancellationToken cancellationToken);

        Task<GetAccountMetadataApiCall> PrepareGetAccountMetadataAsync(CancellationToken cancellationToken);

        Task<UpdateAccountMetadataApiCall> PrepareUpdateAccountMetadataAsync(AccountMetadata metadata, CancellationToken cancellationToken);

        Task<UpdateAccountMetadataApiCall> PrepareRemoveAccountMetadataAsync(IEnumerable<string> keys, CancellationToken cancellationToken);

        #endregion

        #region Containers

        Task<CreateContainerApiCall> PrepareCreateContainerAsync(ContainerName container, CancellationToken cancellationToken);

        Task<RemoveContainerApiCall> PrepareRemoveContainerAsync(ContainerName container, CancellationToken cancellationToken);

        Task<ListObjectsApiCall> PrepareListObjectsAsync(ContainerName container, CancellationToken cancellationToken);

        Task<GetContainerMetadataApiCall> PrepareGetContainerMetadataAsync(ContainerName container, CancellationToken cancellationToken);

        Task<UpdateContainerMetadataApiCall> PrepareUpdateContainerMetadataAsync(ContainerName container, ContainerMetadata metadata, CancellationToken cancellationToken);

        Task<UpdateContainerMetadataApiCall> PrepareRemoveContainerMetadataAsync(ContainerName container, IEnumerable<string> keys, CancellationToken cancellationToken);

        #endregion

        #region Objects

        Task<CreateObjectApiCall> PrepareCreateObjectAsync(ContainerName container, ObjectName @object, Stream stream, CancellationToken cancellationToken, IProgress<long> progress);

        Task<CopyObjectApiCall> PrepareCopyObjectAsync(ContainerName sourceContainer, ObjectName sourceObject, ContainerName destinationContainer, ObjectName destinationObject, CancellationToken cancellationToken);

        Task<RemoveObjectApiCall> PrepareRemoveObjectAsync(ContainerName container, ObjectName @object, CancellationToken cancellationToken);

        Task<GetObjectApiCall> PrepareGetObjectAsync(ContainerName container, ObjectName @object, CancellationToken cancellationToken);

        Task<GetObjectMetadataApiCall> PrepareGetObjectMetadataAsync(ContainerName container, ObjectName @object, CancellationToken cancellationToken);

        Task<SetObjectMetadataApiCall> PrepareSetObjectMetadataAsync(ContainerName container, ObjectName @object, ObjectMetadata metadata, CancellationToken cancellationToken);

        #endregion
    }
}
