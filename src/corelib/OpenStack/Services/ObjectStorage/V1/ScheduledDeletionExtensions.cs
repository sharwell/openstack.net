namespace OpenStack.Services.ObjectStorage.V1
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using OpenStack.Net;
    using Rackspace.Threading;

    public static class ScheduledDeletionExtensions
    {
        public static readonly string DeleteAfter = "X-Delete-After";

        public static readonly string DeleteAt = "X-Delete-At";

        private static readonly DateTimeOffset Epoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);

        public static Task<CreateObjectApiCall> WithDeleteAfter(this Task<CreateObjectApiCall> task, TimeSpan timeSpan)
        {
            return task.WithDeleteAfterImpl(timeSpan);
        }

        public static Task<CopyObjectApiCall> WithDeleteAfter(this Task<CopyObjectApiCall> task, TimeSpan timeSpan)
        {
            return task.WithDeleteAfterImpl(timeSpan);
        }

        public static Task<SetObjectMetadataApiCall> WithDeleteAfter(this Task<SetObjectMetadataApiCall> task, TimeSpan timeSpan)
        {
            return task.WithDeleteAfterImpl(timeSpan);
        }

        public static Task<CreateObjectApiCall> WithDeleteAt(this Task<CreateObjectApiCall> task, DateTimeOffset time)
        {
            return task.WithDeleteAtImpl(time);
        }

        public static Task<SetObjectMetadataApiCall> WithDeleteAt(this Task<SetObjectMetadataApiCall> task, DateTimeOffset time)
        {
            return task.WithDeleteAtImpl(time);
        }

        public static DateTimeOffset? GetScheduledDeletionTime(this ObjectMetadata metadata)
        {
            string stringValue;
            if (!metadata.Headers.TryGetValue(DeleteAt, out stringValue))
                return null;

            int timestamp;
            if (!int.TryParse(stringValue, out timestamp))
                return null;

            return Epoch.AddSeconds(timestamp);
        }

        public static Task<DateTimeOffset?> GetScheduledDeletionTimeAsync(this IObjectStorageService client, ContainerName container, ObjectName @object, CancellationToken cancellationToken)
        {
            return client.GetObjectMetadataAsync(container, @object, cancellationToken)
                .Select(task => GetScheduledDeletionTime(task.Result));
        }

        public static Task SetDeleteAfterAsync(this IObjectStorageService client, ContainerName container, ObjectName @object, TimeSpan timeSpan, CancellationToken cancellationToken)
        {
            var headers =
                new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { DeleteAfter, ((int)timeSpan.TotalSeconds).ToString() }
                };
            ObjectMetadata metadata = new ObjectMetadata(headers, new Dictionary<string, string>());
            return client.UpdateObjectMetadataAsync(container, @object, metadata, cancellationToken);
        }

        public static Task SetDeleteAtAsync(this IObjectStorageService client, ContainerName container, ObjectName @object, DateTimeOffset time, CancellationToken cancellationToken)
        {
            var headers =
                new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { DeleteAt, ((int)(time - Epoch).TotalSeconds).ToString() }
                };
            ObjectMetadata metadata = new ObjectMetadata(headers, new Dictionary<string, string>());
            return client.UpdateObjectMetadataAsync(container, @object, metadata, cancellationToken);
        }

        public static Task RemoveScheduledDeletionTimeAsync(this IObjectStorageService client, ContainerName container, ObjectName @object, CancellationToken cancellationToken)
        {
            var headers =
                new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { DeleteAt, string.Empty }
                };
            ObjectMetadata metadata = new ObjectMetadata(headers, new Dictionary<string, string>());
            return client.UpdateObjectMetadataAsync(container, @object, metadata, cancellationToken);
        }

        private static Task<TCall> WithDeleteAfterImpl<TCall>(this Task<TCall> task, TimeSpan timeSpan)
            where TCall : IHttpApiRequest
        {
            return task.Select(
                innerTask =>
                {
                    task.Result.RequestMessage.Headers.Remove(DeleteAfter);
                    task.Result.RequestMessage.Headers.Add(DeleteAfter, ((int)timeSpan.TotalSeconds).ToString());
                    return task.Result;
                });
        }

        private static Task<TCall> WithDeleteAtImpl<TCall>(this Task<TCall> task, DateTimeOffset time)
            where TCall : IHttpApiRequest
        {
            return task.Select(
                innerTask =>
                {
                    int timestamp = (int)((time - Epoch).TotalSeconds);
                    task.Result.RequestMessage.Headers.Remove(DeleteAt);
                    task.Result.RequestMessage.Headers.Add(DeleteAt, timestamp.ToString());
                    return task.Result;
                });
        }
    }
}
