namespace OpenStack.Services.ObjectStorage.V1
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using OpenStack.Net;
    using Rackspace.Net;
    using Rackspace.Threading;

    public static class ListObjectsExtensions
    {
        public static Task<ListObjectsApiCall> WithPageSize(this Task<ListObjectsApiCall> task, int pageSize)
        {
            return task.WithQueryParameter("limit", pageSize.ToString());
        }

        public static Task<ListObjectsApiCall> WithDelimiter(this Task<ListObjectsApiCall> task, char delimiter)
        {
            return task.WithQueryParameter("delimiter", delimiter.ToString());
        }

        public static Task<ListObjectsApiCall> WithDelimiter(this Task<ListObjectsApiCall> task, ObjectName prefix, char delimiter)
        {
            return task.WithPrefix(prefix).WithDelimiter(delimiter);
        }

        public static Task<ListObjectsApiCall> WithPath(this Task<ListObjectsApiCall> task, ObjectName path)
        {
            return task.WithQueryParameter("path", path.Value);
        }

        public static Task<ListObjectsApiCall> WithPrefix(this Task<ListObjectsApiCall> task, ObjectName prefix)
        {
            return task.WithQueryParameter("prefix", prefix.Value);
        }

        public static Task<ListObjectsApiCall> WithMarker(this Task<ListObjectsApiCall> task, ObjectName marker)
        {
            return task.WithQueryParameter("marker", marker.Value);
        }

        public static Task<ListObjectsApiCall> WithEndMarker(this Task<ListObjectsApiCall> task, ObjectName endMarker)
        {
            return task.WithQueryParameter("end_marker", endMarker.Value);
        }

        public static Task<ListObjectsApiCall> PrepareListObjectsInDirectoryAsync(this IObjectStorageService client, ContainerName container, ObjectName path, CancellationToken cancellationToken)
        {
            return client.PrepareListObjectsAsync(container, cancellationToken)
                .WithPath(path);
        }

        public static Task<ListObjectsApiCall> PrepareListObjectsInDirectoryAsync(this IObjectStorageService client, ContainerName container, ObjectName path, char delimiter, CancellationToken cancellationToken)
        {
            return client.PrepareListObjectsAsync(container, cancellationToken)
                .WithDelimiter(path, delimiter);
        }

        private static Task<TCall> WithQueryParameter<TCall>(this Task<TCall> task, string parameter, string value)
            where TCall : IHttpApiRequest
        {
            return task.Select(
                innerTask =>
                {
                    Uri requestUri = innerTask.Result.RequestMessage.RequestUri;
                    requestUri = UriUtility.RemoveQueryParameter(requestUri, parameter);
                    string originalQuery = requestUri.Query;

                    UriTemplate queryTemplate;
                    if (string.IsNullOrEmpty(originalQuery))
                    {
                        // URI does not already contain query parameters
                        queryTemplate = new UriTemplate("{?" + parameter + "}");
                    }
                    else
                    {
                        // URI already contains query parameters
                        queryTemplate = new UriTemplate("{&" + parameter + "}");
                    }

                    var parameters = new Dictionary<string, string> { { parameter, value } };
                    Uri queryUri = queryTemplate.BindByName(parameters);
                    innerTask.Result.RequestMessage.RequestUri = new Uri(requestUri.OriginalString + queryUri.OriginalString, UriKind.Absolute);
                    return innerTask.Result;
                });
        }
    }
}
