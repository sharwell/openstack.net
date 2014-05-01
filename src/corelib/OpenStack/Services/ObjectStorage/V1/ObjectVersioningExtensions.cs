namespace OpenStack.Services.ObjectStorage.V1
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using OpenStack.Net;
    using Rackspace.Threading;

    public static class ObjectVersioningExtensions
    {
        public static readonly string VersionsLocation = "X-Versions-Location";

        public static ContainerName GetVersionsLocation(this ContainerMetadata metadata)
        {
            string location;
            if (!metadata.Headers.TryGetValue(VersionsLocation, out location) || string.IsNullOrEmpty(location))
                return null;

            // first, URL-decode the value
            location = UriUtility.UriDecode(location);

            // then UTF-8 decode the value
            location = StorageMetadata.DecodeHeaderValue(location);

            // then return the result as a ContainerName
            return new ContainerName(location);
        }

        public static Task<CreateContainerApiCall> WithVersionsLocation(this Task<CreateContainerApiCall> task, ContainerName versionsLocation)
        {
            if (task == null)
                throw new ArgumentNullException("task");
            if (versionsLocation == null)
                throw new ArgumentNullException("versionsLocation");

            return task.WithVersionsLocationImpl(versionsLocation);
        }

        public static Task<UpdateContainerMetadataApiCall> WithVersionsLocation(this Task<UpdateContainerMetadataApiCall> task, ContainerName versionsLocation)
        {
            if (task == null)
                throw new ArgumentNullException("task");
            // allow location to be null as a way to remove the X-Versions-Location header

            return task.WithVersionsLocationImpl(versionsLocation);
        }

        public static Task<UpdateContainerMetadataApiCall> PrepareSetVersionsLocationAsync(this IObjectStorageService service, ContainerName container, ContainerName versionsLocation, CancellationToken cancellationToken)
        {
            return service.PrepareUpdateContainerMetadataAsync(container, ContainerMetadata.Empty, CancellationToken.None)
                .WithVersionsLocation(versionsLocation);
        }

        public static Task<UpdateContainerMetadataApiCall> PrepareRemoveVersionsLocationAsync(this IObjectStorageService service, ContainerName container, CancellationToken cancellationToken)
        {
            return service.PrepareUpdateContainerMetadataAsync(container, ContainerMetadata.Empty, CancellationToken.None)
                .WithVersionsLocation(null);
        }

        public static Task CreateVersionedContainerAsync(this IObjectStorageService service, ContainerName container, ContainerName versionsLocation, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareCreateContainerAsync(container, cancellationToken).WithVersionsLocation(versionsLocation),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task<ContainerName> GetVersionsLocationAsync(this IObjectStorageService service, ContainerName container, CancellationToken cancellationToken)
        {
            return service.GetContainerMetadataAsync(container, cancellationToken)
                .Select(task => GetVersionsLocation(task.Result));
        }

        public static Task SetVersionsLocationAsync(this IObjectStorageService service, ContainerName container, ContainerName versionsLocation, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareSetVersionsLocationAsync(container, versionsLocation, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task RemoveVersionsLocationAsync(this IObjectStorageService service, ContainerName container, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareRemoveVersionsLocationAsync(container, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        private static Task<TCall> WithVersionsLocationImpl<TCall>(this Task<TCall> task, ContainerName location)
            where TCall : IHttpApiRequest
        {
            string encodedLocation = string.Empty;
            if (location != null)
            {
                encodedLocation = location.Value;

                // first, UTF-8 encode the value
                encodedLocation = StorageMetadata.EncodeHeaderValue(encodedLocation);

                // then, URL-encode the value
                encodedLocation = UriUtility.UriEncode(encodedLocation, UriPart.Any);
            }

            return
                task
                .Select(
                    innerTask =>
                    {
                        innerTask.Result.RequestMessage.Headers.Add(VersionsLocation, encodedLocation);
                        return innerTask.Result;
                    });
        }
    }
}
