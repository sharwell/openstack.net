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

        public static Task<HttpApiCall> PrepareCreateVersionedContainerAsync(this IObjectStorageService service, ContainerName container, ContainerName versionsLocation, CancellationToken cancellationToken)
        {
            return service.PrepareCreateContainerAsync(container, CancellationToken.None)
                .WithVersionsLocation(versionsLocation);
        }

        public static Task<HttpApiCall> PrepareSetVersionsLocationAsync(this IObjectStorageService service, ContainerName container, ContainerName versionsLocation, CancellationToken cancellationToken)
        {
            return service.PrepareUpdateContainerMetadataAsync(container, ContainerMetadata.Empty, CancellationToken.None)
                .WithVersionsLocation(versionsLocation);
        }

        public static Task<HttpApiCall> PrepareRemoveVersionsLocationAsync(this IObjectStorageService service, ContainerName container, CancellationToken cancellationToken)
        {
            return service.PrepareUpdateContainerMetadataAsync(container, ContainerMetadata.Empty, CancellationToken.None)
                .WithVersionsLocation(null);
        }

        public static Task CreateVersionedContainerAsync(this IObjectStorageService service, ContainerName container, ContainerName versionsLocation, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareCreateVersionedContainerAsync(container, versionsLocation, cancellationToken),
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

        private static Task<HttpApiCall> WithVersionsLocation(this Task<HttpApiCall> apiCall, ContainerName location)
        {
            if (apiCall == null)
                throw new ArgumentNullException("apiCall");
            // allow location to be null as a way to remove the X-Versions-Location header

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
                apiCall
                .Select(
                    task =>
                    {
                        task.Result.RequestMessage.Headers.Add(VersionsLocation, encodedLocation);
                        return task.Result;
                    });
        }
    }
}
