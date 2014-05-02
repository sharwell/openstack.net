namespace OpenStack.Services.ObjectStorage.V1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;
    using Rackspace.Threading;

    public static class LargeObjectExtensions
    {
        public static Task<long?> GetStaticLargeObjectMaxSegmentCountAsync(this IObjectStorageService client, CancellationToken cancellationToken)
        {
            return
                client.GetObjectStorageInfoAsync(cancellationToken)
                .Select(
                    task =>
                    {
                        JObject slo;
                        if (!task.Result.TryGetValue("slo", out slo))
                            return null;

                        JToken value;
                        if (!slo.TryGetValue("max_manifest_segments", out value))
                            return null;

                        return value.ToObject<long?>();
                    });
        }

        public static Task<long?> GetStaticLargeObjectMinSegmentSizeAsync(this IObjectStorageService client, CancellationToken cancellationToken)
        {
            return
                client.GetObjectStorageInfoAsync(cancellationToken)
                .Select(
                    task =>
                    {
                        JObject slo;
                        if (!task.Result.TryGetValue("slo", out slo))
                            return null;

                        JToken value;
                        if (!slo.TryGetValue("min_segment_size", out value))
                            return null;

                        return value.ToObject<long?>();
                    });
        }

        public static Task<long?> GetStaticLargeObjectMaxManifestSizeAsync(this IObjectStorageService client, CancellationToken cancellationToken)
        {
            return
                client.GetObjectStorageInfoAsync(cancellationToken)
                .Select(
                    task =>
                    {
                        JObject slo;
                        if (!task.Result.TryGetValue("slo", out slo))
                            return null;

                        JToken value;
                        if (!slo.TryGetValue("max_manifest_size", out value))
                            return null;

                        return value.ToObject<long?>();
                    });
        }


        public static Task<long?> GetMaxObjectSize(this IObjectStorageService client, CancellationToken cancellationToken)
        {
            return
                client.GetObjectStorageInfoAsync(cancellationToken)
                .Select(
                    task =>
                    {
                        JObject swift;
                        if (!task.Result.TryGetValue("swift", out swift))
                            return null;

                        JToken value;
                        if (!swift.TryGetValue("max_file_size", out value))
                            return null;

                        return value.ToObject<long?>();
                    });
        }
    }
}
