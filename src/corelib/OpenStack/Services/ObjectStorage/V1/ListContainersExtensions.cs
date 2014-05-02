namespace OpenStack.Services.ObjectStorage.V1
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using OpenStack.Net;
    using Rackspace.Net;
    using Rackspace.Threading;

    public static class ListContainersExtensions
    {
        public static Task<ListContainersApiCall> WithPageSize(this Task<ListContainersApiCall> task, int pageSize)
        {
            return task.WithQueryParameter("limit", pageSize.ToString());
        }

        public static Task<ListContainersApiCall> WithDelimiter(this Task<ListContainersApiCall> task, char delimiter)
        {
            return task.WithQueryParameter("delimiter", delimiter.ToString());
        }

        public static Task<ListContainersApiCall> WithDelimiter(this Task<ListContainersApiCall> task, ContainerName prefix, char delimiter)
        {
            return task.WithPrefix(prefix).WithDelimiter(delimiter);
        }

        public static Task<ListContainersApiCall> WithPrefix(this Task<ListContainersApiCall> task, ContainerName prefix)
        {
            return task.WithQueryParameter("prefix", prefix.Value);
        }

        public static Task<ListContainersApiCall> WithMarker(this Task<ListContainersApiCall> task, ContainerName marker)
        {
            return task.WithQueryParameter("marker", marker.Value);
        }

        public static Task<ListContainersApiCall> WithEndMarker(this Task<ListContainersApiCall> task, ContainerName endMarker)
        {
            return task.WithQueryParameter("end_marker", endMarker.Value);
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
