namespace OpenStack.Services.ObjectStorage.V1
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using OpenStack.Net;
    using Rackspace.Threading;

    public static class StaticWebsiteExtensions
    {
        public static readonly string WebIndex = "Web-Index";

        public static readonly string WebError = "Web-Error";

        public static readonly string WebListings = "Web-Listings";

        public static readonly string WebListingsCss = "Web-Listings-CSS";

        public static Task<bool> SupportsStaticWebsiteAsync(this IObjectStorageService storageService, CancellationToken cancellationToken)
        {
            return storageService.GetObjectStorageInfoAsync(cancellationToken)
                .Select(task => task.Result.ContainsKey("staticweb"));
        }

        public static Task<CreateContainerApiCall> WithWebIndex(this Task<CreateContainerApiCall> task, string indexPage)
        {
            return task.WithContainerMetadata(WebIndex, indexPage);
        }

        public static Task<CreateContainerApiCall> WithWebError(this Task<CreateContainerApiCall> task, string errorPage)
        {
            return task.WithContainerMetadata(WebError, errorPage);
        }

        public static Task<CreateContainerApiCall> WithWebListings(this Task<CreateContainerApiCall> task, bool enableListings)
        {
            if (enableListings)
                return task.WithContainerMetadata(WebListings, "TRUE");
            else
                return task.WithContainerMetadata(WebListings, string.Empty);
        }

        public static Task<CreateContainerApiCall> WithWebListingsStylesheet(this Task<CreateContainerApiCall> task, string listingsCssPage)
        {
            return task.WithContainerMetadata(WebListingsCss, listingsCssPage);
        }

        public static Task<UpdateContainerMetadataApiCall> WithWebIndex(this Task<UpdateContainerMetadataApiCall> task, string indexPage)
        {
            return task.WithContainerMetadata(WebIndex, indexPage);
        }

        public static Task<UpdateContainerMetadataApiCall> WithWebError(this Task<UpdateContainerMetadataApiCall> task, string errorPage)
        {
            return task.WithContainerMetadata(WebError, errorPage);
        }

        public static Task<UpdateContainerMetadataApiCall> WithWebListings(this Task<UpdateContainerMetadataApiCall> task, bool enableListings)
        {
            if (enableListings)
                return task.WithContainerMetadata(WebListings, "TRUE");
            else
                return task.WithContainerMetadata(WebListings, string.Empty);
        }

        public static Task<UpdateContainerMetadataApiCall> WithWebListingsStylesheet(this Task<UpdateContainerMetadataApiCall> task, string listingsCssPage)
        {
            return task.WithContainerMetadata(WebListingsCss, listingsCssPage);
        }

        public static string GetWebIndexPage(this ContainerMetadata metadata)
        {
            string value;
            if (!metadata.Metadata.TryGetValue(WebIndex, out value))
                return null;

            return value;
        }

        public static string GetWebErrorPage(this ContainerMetadata metadata)
        {
            string value;
            if (!metadata.Metadata.TryGetValue(WebError, out value))
                return null;

            return value;
        }

        public static bool GetWebListingsEnabled(this ContainerMetadata metadata)
        {
            string value;
            if (!metadata.Metadata.TryGetValue(WebListings, out value))
                return false;

            return string.Equals(value, "true", StringComparison.OrdinalIgnoreCase);
        }

        public static string GetWebListingsStylesheet(this ContainerMetadata metadata)
        {
            string value;
            if (!metadata.Metadata.TryGetValue(WebListingsCss, out value))
                return null;

            return value;
        }

        public static Task<CreateContainerApiCall> PrepareCreateStaticWebsiteContainerAsync(this IObjectStorageService client, ContainerName container, string indexPage, string errorPage, CancellationToken cancellationToken)
        {
            return client.PrepareCreateContainerAsync(container, cancellationToken)
                .WithWebIndex(indexPage)
                .WithWebError(errorPage);
        }

        public static Task<CreateContainerApiCall> PrepareCreateStaticWebsiteContainerAsync(this IObjectStorageService client, ContainerName container, string indexPage, string errorPage, string listingsCssPage, CancellationToken cancellationToken)
        {
            return client.PrepareCreateStaticWebsiteContainerAsync(container, indexPage, errorPage, cancellationToken)
                .WithWebListings(true)
                .WithWebListingsStylesheet(listingsCssPage);
        }

        public static Task<UpdateContainerMetadataApiCall> PrepareSetStaticWebsiteAsync(this IObjectStorageService client, ContainerName container, string indexPage, string errorPage, CancellationToken cancellationToken)
        {
            return client.PrepareUpdateContainerMetadataAsync(container, ContainerMetadata.Empty, cancellationToken)
                .WithWebIndex(indexPage)
                .WithWebError(errorPage)
                .WithWebListings(false)
                .WithContainerMetadata(WebListingsCss, string.Empty);
        }

        public static Task<UpdateContainerMetadataApiCall> PrepareSetStaticWebsiteAsync(this IObjectStorageService client, ContainerName container, string indexPage, string errorPage, string listingsCssPage, CancellationToken cancellationToken)
        {
            return client.PrepareUpdateContainerMetadataAsync(container, ContainerMetadata.Empty, cancellationToken)
                .WithWebIndex(indexPage)
                .WithWebError(errorPage)
                .WithWebListings(true)
                .WithWebListingsStylesheet(listingsCssPage);
        }

        public static Task<UpdateContainerMetadataApiCall> PrepareSetWebIndexAsync(this IObjectStorageService client, ContainerName container, string indexPage, CancellationToken cancellationToken)
        {
            return client.PrepareUpdateContainerMetadataAsync(container, ContainerMetadata.Empty, cancellationToken)
                .WithWebIndex(indexPage);
        }

        public static Task<UpdateContainerMetadataApiCall> PrepareRemoveWebIndexAsync(this IObjectStorageService client, ContainerName container, CancellationToken cancellationToken)
        {
            return client.PrepareUpdateContainerMetadataAsync(container, ContainerMetadata.Empty, cancellationToken)
                .WithContainerMetadata(WebIndex, string.Empty);
        }

        public static Task<UpdateContainerMetadataApiCall> PrepareSetWebErrorAsync(this IObjectStorageService client, ContainerName container, string errorPage, CancellationToken cancellationToken)
        {
            return client.PrepareUpdateContainerMetadataAsync(container, ContainerMetadata.Empty, cancellationToken)
                .WithWebError(errorPage);
        }

        public static Task<UpdateContainerMetadataApiCall> PrepareRemoveWebErrorAsync(this IObjectStorageService client, ContainerName container, CancellationToken cancellationToken)
        {
            return client.PrepareUpdateContainerMetadataAsync(container, ContainerMetadata.Empty, cancellationToken)
                .WithContainerMetadata(WebError, string.Empty);
        }

        public static Task<UpdateContainerMetadataApiCall> PrepareSetWebListingsStylesheetAsync(this IObjectStorageService client, ContainerName container, string listingsCssPage, CancellationToken cancellationToken)
        {
            return client.PrepareUpdateContainerMetadataAsync(container, ContainerMetadata.Empty, cancellationToken)
                .WithWebListings(true)
                .WithWebListingsStylesheet(listingsCssPage);
        }

        public static Task<UpdateContainerMetadataApiCall> PrepareRemoveWebListingsStylesheetAsync(this IObjectStorageService client, ContainerName container, CancellationToken cancellationToken)
        {
            return client.PrepareUpdateContainerMetadataAsync(container, ContainerMetadata.Empty, cancellationToken)
                .WithWebListings(false)
                .WithContainerMetadata(WebListingsCss, string.Empty);
        }

        public static Task CreateStaticWebsiteContainerAsync(this IObjectStorageService client, ContainerName container, string indexPage, string errorPage, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => client.PrepareCreateStaticWebsiteContainerAsync(container, indexPage, errorPage, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task CreateStaticWebsiteContainerAsync(this IObjectStorageService client, ContainerName container, string indexPage, string errorPage, string listingsCssPage, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => client.PrepareCreateStaticWebsiteContainerAsync(container, indexPage, errorPage, listingsCssPage, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task SetStaticWebsiteAsync(this IObjectStorageService client, ContainerName container, string indexPage, string errorPage, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => client.PrepareSetStaticWebsiteAsync(container, indexPage, errorPage, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task SetStaticWebsiteAsync(this IObjectStorageService client, ContainerName container, string indexPage, string errorPage, string listingsCssPage, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => client.PrepareSetStaticWebsiteAsync(container, indexPage, errorPage, listingsCssPage, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task SetWebIndexAsync(this IObjectStorageService client, ContainerName container, string indexPage, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => client.PrepareSetWebIndexAsync(container, indexPage, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task RemoveWebIndexAsync(this IObjectStorageService client, ContainerName container, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => client.PrepareRemoveWebIndexAsync(container, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task SetWebErrorAsync(this IObjectStorageService client, ContainerName container, string errorPage, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => client.PrepareSetWebErrorAsync(container, errorPage, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task RemoveWebErrorAsync(this IObjectStorageService client, ContainerName container, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => client.PrepareRemoveWebErrorAsync(container, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task SetWebListingsStylesheetAsync(this IObjectStorageService client, ContainerName container, string listingsCssPage, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => client.PrepareSetWebListingsStylesheetAsync(container, listingsCssPage, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task RemoveWebListingsStylesheetAsync(this IObjectStorageService client, ContainerName container, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => client.PrepareRemoveWebListingsStylesheetAsync(container, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        private static Task<TCall> WithContainerMetadata<TCall>(this Task<TCall> task, string metadata, string value)
            where TCall : IHttpApiRequest
        {
            if (task == null)
                throw new ArgumentNullException("task");
            if (metadata == null)
                throw new ArgumentNullException("metadata");
            if (value == null)
                throw new ArgumentNullException("value");
            if (string.IsNullOrEmpty(metadata))
                throw new ArgumentException("metadata cannot be empty");
            // allow empty value as a means of removing the metadata

            return
                task.Select(
                    innerTask =>
                    {
                        task.Result.RequestMessage.Headers.Remove(ContainerMetadata.ContainerMetadataPrefix + metadata);
                        task.Result.RequestMessage.Headers.Add(ContainerMetadata.ContainerMetadataPrefix + metadata, value);
                        return task.Result;
                    });
        }
    }
}
