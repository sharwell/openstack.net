namespace OpenStack.Services.ObjectStorage.V1
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;
    using OpenStack.Collections;
    using OpenStack.Net;
    using Stream = System.IO.Stream;

#if !NET40PLUS
    using OpenStack.Compat;
    using Rackspace.Threading;
#endif

    public interface IObjectStorageService
    {
        #region Discoverability

        Task<HttpApiCall<ReadOnlyDictionary<string, JObject>>> PrepareGetObjectStorageInfoAsync(CancellationToken cancellationToken);

        #endregion

        #region Accounts

        Task<HttpApiCall<Tuple<AccountMetadata, ReadOnlyCollectionPage<Container>>>> PrepareListContainersAsync(int? pageSize, CancellationToken cancellationToken);

        Task<HttpApiCall<AccountMetadata>> PrepareGetAccountMetadataAsync(CancellationToken cancellationToken);

        Task<HttpApiCall> PrepareUpdateAccountMetadataAsync(AccountMetadata metadata, CancellationToken cancellationToken);

        Task<HttpApiCall> PrepareRemoveAccountMetadataAsync(IEnumerable<string> keys, CancellationToken cancellationToken);

        #endregion

        #region Containers

        Task<HttpApiCall> PrepareCreateContainerAsync(ContainerName container, CancellationToken cancellationToken);

        Task<HttpApiCall> PrepareRemoveContainerAsync(ContainerName container, CancellationToken cancellationToken);

        Task<HttpApiCall<Tuple<ContainerMetadata, ReadOnlyCollectionPage<Object>>>> PrepareListObjectsAsync(ContainerName container, CancellationToken cancellationToken);

        Task<HttpApiCall<ContainerMetadata>> PrepareGetContainerMetadataAsync(ContainerName container, CancellationToken cancellationToken);

        Task<HttpApiCall> PrepareUpdateContainerMetadataAsync(ContainerName container, ContainerMetadata metadata, CancellationToken cancellationToken);

        Task<HttpApiCall> PrepareRemoveContainerMetadataAsync(ContainerName container, IEnumerable<string> keys, CancellationToken cancellationToken);

        #endregion

        #region Objects

        Task<HttpApiCall> PrepareCreateObjectAsync(ContainerName container, ObjectName @object, Stream stream, CancellationToken cancellationToken, IProgress<long> progress);

        Task<HttpApiCall> PrepareCopyObjectAsync(ContainerName sourceContainer, ObjectName sourceObject, ContainerName destinationContainer, ObjectName destinationObject, CancellationToken cancellationToken);

        Task<HttpApiCall> PrepareRemoveObjectAsync(ContainerName container, ObjectName @object, CancellationToken cancellationToken);

        Task<HttpApiCall<Tuple<ObjectMetadata, Stream>>> PrepareGetObjectAsync(ContainerName container, ObjectName @object, CancellationToken cancellationToken);

        Task<HttpApiCall<ObjectMetadata>> PrepareGetObjectMetadataAsync(ContainerName container, ObjectName @object, CancellationToken cancellationToken);

        Task<HttpApiCall> PrepareUpdateObjectMetadataAsync(ContainerName container, ObjectName @object, ObjectMetadata metadata, CancellationToken cancellationToken);

        Task<HttpApiCall> PrepareRemoveObjectMetadataAsync(ContainerName container, ObjectName @object, IEnumerable<string> keys, CancellationToken cancellationToken);

        #endregion
    }
}
