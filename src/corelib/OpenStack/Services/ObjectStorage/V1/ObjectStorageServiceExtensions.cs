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
    using Rackspace.Threading;
    using Stream = System.IO.Stream;

#if !NET40PLUS
    using OpenStack.Compat;
#endif

    public static class ObjectStorageServiceExtensions
    {
        #region Discoverability

        public static Task<ReadOnlyDictionary<string, JObject>> GetObjectStorageInfoAsync(this IObjectStorageService service, CancellationToken cancellationToken)
        {
            return service.PrepareGetObjectStorageInfoAsync(cancellationToken)
                .Then(task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        #endregion

        #region Accounts

        public static Task<Tuple<AccountMetadata, ReadOnlyCollectionPage<Container>>> ListContainersAsync(this IObjectStorageService service, int? pageSize, CancellationToken cancellationToken)
        {
            return service.PrepareListContainersAsync(pageSize, cancellationToken)
                .Then(task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<AccountMetadata> GetAccountMetadataAsync(this IObjectStorageService service, CancellationToken cancellationToken)
        {
            return service.PrepareGetAccountMetadataAsync(cancellationToken)
                .Then(task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task UpdateAccountMetadataAsync(this IObjectStorageService service, AccountMetadata metadata, CancellationToken cancellationToken)
        {
            return service.PrepareUpdateAccountMetadataAsync(metadata, cancellationToken)
                .Then(task => task.Result.SendAsync(cancellationToken));
        }

        public static Task RemoveAccountMetadataAsync(this IObjectStorageService service, IEnumerable<string> keys, CancellationToken cancellationToken)
        {
            return service.PrepareRemoveAccountMetadataAsync(keys, cancellationToken)
                .Then(task => task.Result.SendAsync(cancellationToken));
        }

        #endregion

        #region Containers

        public static Task CreateContainerAsync(this IObjectStorageService service, ContainerName container, CancellationToken cancellationToken)
        {
            return service.PrepareCreateContainerAsync(container, cancellationToken)
                .Then(task => task.Result.SendAsync(cancellationToken));
        }

        public static Task RemoveContainerAsync(this IObjectStorageService service, ContainerName container, CancellationToken cancellationToken)
        {
            return service.PrepareRemoveContainerAsync(container, cancellationToken)
                .Then(task => task.Result.SendAsync(cancellationToken));
        }

        public static Task<Tuple<ContainerMetadata, ReadOnlyCollectionPage<Object>>> ListObjectsAsync(this IObjectStorageService service, ContainerName container, CancellationToken cancellationToken)
        {
            return service.PrepareListObjectsAsync(container, cancellationToken)
                .Then(task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<ContainerMetadata> GetContainerMetadataAsync(this IObjectStorageService service, ContainerName container, CancellationToken cancellationToken)
        {
            return service.PrepareGetContainerMetadataAsync(container, cancellationToken)
                .Then(task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task UpdateContainerMetadataAsync(this IObjectStorageService service, ContainerName container, ContainerMetadata metadata, CancellationToken cancellationToken)
        {
            return service.PrepareUpdateContainerMetadataAsync(container, metadata, cancellationToken)
                .Then(task => task.Result.SendAsync(cancellationToken));
        }

        public static Task RemoveContainerMetadataAsync(this IObjectStorageService service, ContainerName container, IEnumerable<string> keys, CancellationToken cancellationToken)
        {
            return service.PrepareRemoveContainerMetadataAsync(container, keys, cancellationToken)
              .Then(task => task.Result.SendAsync(cancellationToken));
        }

        #endregion

        #region Objects

        public static Task CreateObjectAsync(this IObjectStorageService service, ContainerName container, ObjectName @object, Stream stream, CancellationToken cancellationToken, IProgress<long> progress)
        {
            return service.PrepareCreateObjectAsync(container, @object, stream, cancellationToken, progress)
                .Then(task => task.Result.SendAsync(cancellationToken));
        }

        public static Task CopyObjectAsync(this IObjectStorageService service, ContainerName sourceContainer, ObjectName sourceObject, ContainerName destinationContainer, ObjectName destinationObject, CancellationToken cancellationToken)
        {
            return service.PrepareCopyObjectAsync(sourceContainer, sourceObject, destinationContainer, destinationObject, cancellationToken)
                .Then(task => task.Result.SendAsync(cancellationToken));
        }

        public static Task RemoveObjectAsync(this IObjectStorageService service, ContainerName container, ObjectName @object, CancellationToken cancellationToken)
        {
            return service.PrepareRemoveObjectAsync(container, @object, cancellationToken)
                .Then(task => task.Result.SendAsync(cancellationToken));
        }

        public static Task<Tuple<ObjectMetadata, Stream>> GetObjectAsync(this IObjectStorageService service, ContainerName container, ObjectName @object, CancellationToken cancellationToken)
        {
            return service.PrepareGetObjectAsync(container, @object, cancellationToken)
                .Then(task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<ObjectMetadata> GetObjectMetadataAsync(this IObjectStorageService service, ContainerName container, ObjectName @object, CancellationToken cancellationToken)
        {
            return service.PrepareGetObjectMetadataAsync(container, @object, cancellationToken)
                .Then(task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task UpdateObjectMetadataAsync(this IObjectStorageService service, ContainerName container, ObjectName @object, ObjectMetadata metadata, CancellationToken cancellationToken)
        {
            return service.PrepareUpdateObjectMetadataAsync(container, @object, metadata, cancellationToken)
                .Then(task => task.Result.SendAsync(cancellationToken));
        }

        public static Task RemoveObjectMetadataAsync(this IObjectStorageService service, ContainerName container, ObjectName @object, IEnumerable<string> keys, CancellationToken cancellationToken)
        {
            return service.PrepareRemoveObjectMetadataAsync(container, @object, keys, cancellationToken)
                .Then(task => task.Result.SendAsync(cancellationToken));
        }

        #endregion
    }
}
