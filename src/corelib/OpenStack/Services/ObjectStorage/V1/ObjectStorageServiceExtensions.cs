namespace OpenStack.Services.ObjectStorage.V1
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net.Http.Headers;
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

    /// <summary>
    /// This class provides extension methods that simplify the process of preparing and sending
    /// Object Storage Service HTTP API calls for the most common use cases.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public static class ObjectStorageServiceExtensions
    {
        #region Discoverability

        /// <summary>
        /// Determine the features enabled in the Object Storage service.
        /// </summary>
        /// <param name="service">The <see cref="IObjectStorageService"/> instance.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the a dictionary; the keys are the names of properties, and the values are JSON
        /// objects containing arbitrary data associated with the properties.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="service"/> is <see langword="null"/>.</exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <seealso cref="GetObjectStorageInfoApiCall"/>
        /// <seealso cref="IObjectStorageService.PrepareGetObjectStorageInfoAsync"/>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/discoverability.html">Discoverability (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<ReadOnlyDictionary<string, JObject>> GetObjectStorageInfoAsync(this IObjectStorageService service, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetObjectStorageInfoAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        #endregion

        #region Accounts

        public static Task<Tuple<AccountMetadata, ReadOnlyCollectionPage<Container>>> ListContainersAsync(this IObjectStorageService service, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareListContainersAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<AccountMetadata> GetAccountMetadataAsync(this IObjectStorageService service, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetAccountMetadataAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task UpdateAccountMetadataAsync(this IObjectStorageService service, AccountMetadata metadata, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareUpdateAccountMetadataAsync(metadata, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task RemoveAccountMetadataAsync(this IObjectStorageService service, IEnumerable<string> keys, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareRemoveAccountMetadataAsync(keys, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        #endregion

        #region Containers

        public static Task CreateContainerAsync(this IObjectStorageService service, ContainerName container, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareCreateContainerAsync(container, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task RemoveContainerAsync(this IObjectStorageService service, ContainerName container, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareRemoveContainerAsync(container, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task<Tuple<ContainerMetadata, ReadOnlyCollectionPage<ContainerObject>>> ListObjectsAsync(this IObjectStorageService service, ContainerName container, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareListObjectsAsync(container, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<ContainerMetadata> GetContainerMetadataAsync(this IObjectStorageService service, ContainerName container, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetContainerMetadataAsync(container, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task UpdateContainerMetadataAsync(this IObjectStorageService service, ContainerName container, ContainerMetadata metadata, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareUpdateContainerMetadataAsync(container, metadata, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task RemoveContainerMetadataAsync(this IObjectStorageService service, ContainerName container, IEnumerable<string> keys, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareRemoveContainerMetadataAsync(container, keys, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        #endregion

        #region Objects

        public static Task CreateObjectAsync(this IObjectStorageService service, ContainerName container, ObjectName @object, Stream stream, CancellationToken cancellationToken, IProgress<long> progress)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareCreateObjectAsync(container, @object, stream, cancellationToken, progress),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task CopyObjectAsync(this IObjectStorageService service, ContainerName sourceContainer, ObjectName sourceObject, ContainerName destinationContainer, ObjectName destinationObject, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareCopyObjectAsync(sourceContainer, sourceObject, destinationContainer, destinationObject, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task MoveObjectAsync(this IObjectStorageService service, ContainerName sourceContainer, ObjectName sourceObject, ContainerName destinationContainer, ObjectName destinationObject, CancellationToken cancellationToken)
        {
            return service.CopyObjectAsync(sourceContainer, sourceObject, destinationContainer, destinationObject, cancellationToken)
                .Then(task => service.RemoveObjectAsync(sourceContainer, sourceObject, cancellationToken));
        }

        public static Task RemoveObjectAsync(this IObjectStorageService service, ContainerName container, ObjectName @object, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareRemoveObjectAsync(container, @object, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task<Tuple<ObjectMetadata, Stream>> GetObjectAsync(this IObjectStorageService service, ContainerName container, ObjectName @object, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetObjectAsync(container, @object, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<Tuple<ObjectMetadata, Stream>> GetObjectAsync(this IObjectStorageService service, ContainerName container, ObjectName @object, RangeHeaderValue rangeHeader, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetObjectAsync(container, @object, cancellationToken),
                    task =>
                    {
                        task.Result.RequestMessage.Headers.Range = rangeHeader;
                        return task.Result.SendAsync(cancellationToken);
                    })
                .Select(task => task.Result.Item2);
        }

        public static Task<ObjectMetadata> GetObjectMetadataAsync(this IObjectStorageService service, ContainerName container, ObjectName @object, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetObjectMetadataAsync(container, @object, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task SetObjectMetadataAsync(this IObjectStorageService service, ContainerName container, ObjectName @object, ObjectMetadata metadata, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareSetObjectMetadataAsync(container, @object, metadata, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task UpdateObjectMetadataAsync(this IObjectStorageService service, ContainerName container, ObjectName @object, ObjectMetadata metadata, CancellationToken cancellationToken)
        {
            return GetObjectMetadataAsync(service, container, @object, cancellationToken)
                .Then(task => SetObjectMetadataAsync(service, container, @object, MergeMetadata(task.Result, metadata), cancellationToken));
        }

        public static Task RemoveObjectMetadataAsync(this IObjectStorageService service, ContainerName container, ObjectName @object, IEnumerable<string> keys, CancellationToken cancellationToken)
        {
            ObjectMetadata updatedMetadata = new ObjectMetadata(new Dictionary<string, string>(), keys.ToDictionary(i => i, i => string.Empty));
            return UpdateObjectMetadataAsync(service, container, @object, updatedMetadata, cancellationToken);
        }

        #endregion

        private static ObjectMetadata MergeMetadata(ObjectMetadata originalMetadata, ObjectMetadata updatedMetadata)
        {
            Dictionary<string, string> headers = MergeMetadata(originalMetadata.Headers, updatedMetadata.Headers);
            Dictionary<string, string> metadata = MergeMetadata(originalMetadata.Metadata, updatedMetadata.Metadata);
            return new ObjectMetadata(headers, metadata);
        }

        private static Dictionary<string, string> MergeMetadata(IDictionary<string, string> originalMetadata, IDictionary<string, string> updatedMetadata)
        {
            Dictionary<string, string> result = new Dictionary<string, string>(originalMetadata, StringComparer.OrdinalIgnoreCase);
            foreach (var pair in updatedMetadata)
                result[pair.Key] = pair.Value;

            return result;
        }
    }
}
