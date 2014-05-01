namespace OpenStack.Services.ObjectStorage.V1
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Rackspace.Threading;

    public static class ContainerQuotaExtensions
    {
        public static readonly string QuotaBytes = "Quota-Bytes";

        public static readonly string QuotaCount = "Quota-Count";

        public static Task<bool> SupportsContainerQuotasAsync(this IObjectStorageService storageService, CancellationToken cancellationToken)
        {
            return storageService.GetObjectStorageInfoAsync(cancellationToken)
                .Select(task => task.Result.ContainsKey("container_quotas"));
        }

        public static Task<UpdateContainerMetadataApiCall> WithSizeQuota(this Task<UpdateContainerMetadataApiCall> task, long size)
        {
            return
                task.Select(
                    innerTask =>
                    {
                        task.Result.RequestMessage.Headers.Add(ContainerMetadata.ContainerMetadataPrefix + QuotaBytes, size.ToString());
                        return task.Result;
                    });
        }

        public static Task<UpdateContainerMetadataApiCall> WithObjectCountQuota(this Task<UpdateContainerMetadataApiCall> task, long count)
        {
            return
                task.Select(
                    innerTask =>
                    {
                        task.Result.RequestMessage.Headers.Add(ContainerMetadata.ContainerMetadataPrefix + QuotaCount, count.ToString());
                        return task.Result;
                    });
        }

        public static long? GetSizeQuota(this ContainerMetadata metadata)
        {
            string value;
            if (!metadata.Metadata.TryGetValue(QuotaBytes, out value))
                return null;

            long result;
            if (!long.TryParse(value, out result))
                return null;

            return result;
        }

        public static long? GetObjectCountQuota(this ContainerMetadata metadata)
        {
            string value;
            if (!metadata.Metadata.TryGetValue(QuotaCount, out value))
                return null;

            long result;
            if (!long.TryParse(value, out result))
                return null;

            return result;
        }

        public static Task<UpdateContainerMetadataApiCall> PrepareSetContainerQuotaAsync(this IObjectStorageService client, ContainerName container, long? size, long? count, CancellationToken cancellationToken)
        {
            Dictionary<string, string> metadata = new Dictionary<string, string>();
            if (size != null)
                metadata[QuotaBytes] = size.ToString();
            if (count != null)
                metadata[QuotaCount] = count.ToString();

            return client.PrepareUpdateContainerMetadataAsync(container, new ContainerMetadata(new Dictionary<string, string>(), metadata), cancellationToken);
        }

        public static Task<UpdateContainerMetadataApiCall> PrepareRemoveContainerQuotaAsync(this IObjectStorageService client, ContainerName container, CancellationToken cancellationToken)
        {
            string[] keys = { QuotaBytes, QuotaCount };
            return client.PrepareRemoveContainerMetadataAsync(container, keys, cancellationToken);
        }

        public static Task<long?> GetContainerSizeQuotaAsync(this IObjectStorageService service, ContainerName container, CancellationToken cancellationToken)
        {
            return service.GetContainerMetadataAsync(container, cancellationToken)
                .Select(task => GetSizeQuota(task.Result));
        }

        public static Task<long?> GetContainerObjectCountQuotaAsync(this IObjectStorageService service, ContainerName container, CancellationToken cancellationToken)
        {
            return service.GetContainerMetadataAsync(container, cancellationToken)
                .Select(task => GetObjectCountQuota(task.Result));
        }

        public static Task SetContainerQuotaAsync(this IObjectStorageService client, ContainerName container, long? size, long? count, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => client.PrepareSetContainerQuotaAsync(container, size, count, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task RemoveContainerQuotaAsync(this IObjectStorageService client, ContainerName container, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => client.PrepareRemoveContainerQuotaAsync(container, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }
    }
}
