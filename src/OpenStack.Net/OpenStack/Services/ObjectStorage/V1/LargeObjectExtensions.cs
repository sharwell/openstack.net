namespace OpenStack.Services.ObjectStorage.V1
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;
    using OpenStack.Net;
    using Rackspace.Threading;

    /// <summary>
    /// This class provides extension methods for working static and dynamic large objects.
    /// </summary>
    /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/large-object-creation.html">Large objects (OpenStack Object Storage API V1 Reference)</seealso>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public static class LargeObjectExtensions
    {
        /// <summary>
        /// Gets the maximum number of segments allowed for a Static Large Object in the Object Storage Service.
        /// <note type="warning">This method relies on properties which are not defined by OpenStack, and may not be consistent across vendors.</note>
        /// </summary>
        /// <param name="service">The <see cref="IObjectStorageService"/> instance.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task completes
        /// successfully, the <see cref="Task{TResult}.Result"/> property contains the maximum
        /// number of segments allowed for a Static Large Object, or <see langword="null"/> if the
        /// value could not be determined from the information returned by the
        /// <see cref="IObjectStorageService.PrepareGetObjectStorageInfoAsync"/> API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="service"/> is <see langword="null"/>.</exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/discoverability.html">Discoverability (OpenStack Object Storage API V1 Reference)</seealso>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/large-object-creation.html">Static large objects (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<long?> GetStaticLargeObjectMaxSegmentCountAsync(this IObjectStorageService service, CancellationToken cancellationToken)
        {
            return
                service.GetObjectStorageInfoAsync(cancellationToken)
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

        /// <summary>
        /// Gets the minimum size allowed for a single segment of a Static Large Object in the Object Storage Service.
        /// <note type="warning">This method relies on properties which are not defined by OpenStack, and may not be consistent across vendors.</note>
        /// </summary>
        /// <param name="service">The <see cref="IObjectStorageService"/> instance.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task completes
        /// successfully, the <see cref="Task{TResult}.Result"/> property contains the minimum
        /// size allowed for a single segment of a Static Large Object, or <see langword="null"/>
        /// if the value could not be determined from the information returned by the
        /// <see cref="IObjectStorageService.PrepareGetObjectStorageInfoAsync"/> API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="service"/> is <see langword="null"/>.</exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/discoverability.html">Discoverability (OpenStack Object Storage API V1 Reference)</seealso>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/large-object-creation.html">Static large objects (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<long?> GetStaticLargeObjectMinSegmentSizeAsync(this IObjectStorageService service, CancellationToken cancellationToken)
        {
            return
                service.GetObjectStorageInfoAsync(cancellationToken)
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

        /// <summary>
        /// Gets the maximum size of the manifest object listing the segments of a Static Large Object in the Object Storage Service.
        /// <note type="warning">This method relies on properties which are not defined by OpenStack, and may not be consistent across vendors.</note>
        /// </summary>
        /// <param name="service">The <see cref="IObjectStorageService"/> instance.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task completes
        /// successfully, the <see cref="Task{TResult}.Result"/> property contains the maximum
        /// size of the manifest object listing the segments of a Static Large Object, or
        /// <see langword="null"/> if the value could not be determined from the information returned
        /// by the <see cref="IObjectStorageService.PrepareGetObjectStorageInfoAsync"/> API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="service"/> is <see langword="null"/>.</exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/discoverability.html">Discoverability (OpenStack Object Storage API V1 Reference)</seealso>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/large-object-creation.html">Static large objects (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<long?> GetStaticLargeObjectMaxManifestSizeAsync(this IObjectStorageService service, CancellationToken cancellationToken)
        {
            return
                service.GetObjectStorageInfoAsync(cancellationToken)
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

        /// <summary>
        /// Gets the maximum allowed size for a single object in the Object Storage Service.
        /// <note type="warning">This method relies on properties which are not defined by OpenStack, and may not be consistent across vendors.</note>
        /// </summary>
        /// <param name="service">The <see cref="IObjectStorageService"/> instance.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task completes
        /// successfully, the <see cref="Task{TResult}.Result"/> property contains the maximum
        /// allowed size of a single object, or <see langword="null"/> if the value could not be
        /// determined from the information returned by the
        /// <see cref="IObjectStorageService.PrepareGetObjectStorageInfoAsync"/> API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="service"/> is <see langword="null"/>.</exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/discoverability.html">Discoverability (OpenStack Object Storage API V1 Reference)</seealso>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/large-object-creation.html">Large objects (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<long?> GetMaxObjectSize(this IObjectStorageService service, CancellationToken cancellationToken)
        {
            return
                service.GetObjectStorageInfoAsync(cancellationToken)
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
