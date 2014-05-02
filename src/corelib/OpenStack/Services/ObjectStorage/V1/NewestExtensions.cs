namespace OpenStack.Services.ObjectStorage.V1
{
    using System.Threading.Tasks;
    using OpenStack.Net;
    using Rackspace.Threading;

    public static class NewestExtensions
    {
        public static readonly string Newest = "X-Newest";

        public static Task<GetAccountMetadataApiCall> WithNewest(this Task<GetAccountMetadataApiCall> task)
        {
            return task.WithNewestImpl();
        }

        public static Task<GetContainerMetadataApiCall> WithNewest(this Task<GetContainerMetadataApiCall> task)
        {
            return task.WithNewestImpl();
        }

        public static Task<GetObjectMetadataApiCall> WithNewest(this Task<GetObjectMetadataApiCall> task)
        {
            return task.WithNewestImpl();
        }

        public static Task<GetObjectApiCall> WithNewest(this Task<GetObjectApiCall> task)
        {
            return task.WithNewestImpl();
        }

        public static Task<ListContainersApiCall> WithNewest(this Task<ListContainersApiCall> task)
        {
            return task.WithNewestImpl();
        }

        public static Task<ListObjectsApiCall> WithNewest(this Task<ListObjectsApiCall> task)
        {
            return task.WithNewestImpl();
        }

        private static Task<TCall> WithNewestImpl<TCall>(this Task<TCall> task)
            where TCall : IHttpApiRequest
        {
            return task.Select(
                innerTask =>
                {
                    innerTask.Result.RequestMessage.Headers.Remove(Newest);
                    innerTask.Result.RequestMessage.Headers.Add(Newest, "true");
                    return innerTask.Result;
                });
        }
    }
}
