namespace OpenStack.Services.ObjectStorage.V1
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using OpenStack.Net;
    using Rackspace.Threading;
    using Stream = System.IO.Stream;

#if !NET40PLUS
    using OpenStack.Compat;
#endif

    /// <summary>
    /// This class provides extension methods for using the optional Extract Archive functionality
    /// provided by the Object Storage service.
    /// </summary>
    /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/archive-auto-extract.html">Auto-extract archive files (OpenStack Object Storage API V1 Reference)</seealso>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public static class ExtractArchiveExtensions
    {
        /// <summary>
        /// A placeholder container name used in the call to <see cref="IObjectStorageService.PrepareCreateObjectAsync"/>
        /// as part of preparing an Extract Archive API call that does not specify a container name for the extracted
        /// archive content.
        /// </summary>
        private static readonly ContainerName DummyContainerName = new ContainerName("dummyContainer");

        /// <summary>
        /// A placeholder object name used in the call to <see cref="IObjectStorageService.PrepareCreateObjectAsync"/>
        /// as part of preparing an Extract Archive API call that does not specify an object prefix for the extracted
        /// archive content.
        /// </summary>
        private static readonly ObjectName DummyObjectName = new ObjectName("dummyObject");

        /// <summary>
        /// Determines whether a particular Object Storage Service supports the optional Extract Archive operation.
        /// <note type="warning">This method relies on properties which are not defined by OpenStack, and may not be consistent across vendors.</note>
        /// </summary>
        /// <remarks>
        /// If the Object Storage Service supports the Extract Archive operation, but does not support
        /// feature discoverability, this method might return <see langword="false"/> or result in an
        /// <see cref="HttpWebException"/> even though the Extract Archive operation is supported. To
        /// ensure this situation does not prevent the use of the Extract Archive operation, it is not
        /// automatically checked prior to sending an Extract Archive API call.
        /// </remarks>
        /// <param name="service">The <see cref="IObjectStorageService"/> instance.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task completes
        /// successfully, the <see cref="Task{TResult}.Result"/> property contains a value
        /// indicating whether or not the service supports the Extract Archive operation.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="service"/> is <see langword="null"/>.</exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/discoverability.html">Discoverability (OpenStack Object Storage API V1 Reference)</seealso>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/archive-auto-extract.html">Auto-extract archive files (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<bool> SupportsExtractArchiveAsync(this IObjectStorageService service, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return service.GetObjectStorageInfoAsync(cancellationToken)
                .Select(task => task.Result.ContainsKey("bulk_upload"));
        }

        /// <summary>
        /// Upload an archive and extract the contained files to create objects in the Object Storage Service.
        /// The root folders in the archive specify the container names in which the objects are created.
        /// </summary>
        /// <param name="service">The <see cref="IObjectStorageService"/> instance.</param>
        /// <param name="stream">A stream providing the raw data of the archive to upload and extract.</param>
        /// <param name="format">An <see cref="ArchiveFormat"/> instance describing the file format of the data in <paramref name="stream"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <param name="progress">An <see cref="IProgress{T}"/> instance to notify about progress of sending data to the Object Storage Service, or <see langword="null"/> if progress updates are not required.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task completes
        /// successfully, the <see cref="Task{TResult}.Result"/> property contains an
        /// <see cref="ExtractArchiveResponse"/> instance describing the results of the Extract
        /// Archive operation.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="stream"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="format"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/archive-auto-extract.html">Auto-extract archive files (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<ExtractArchiveResponse> ExtractArchiveAsync(this IObjectStorageService service, Stream stream, ArchiveFormat format, CancellationToken cancellationToken, IProgress<long> progress)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return service.ExtractArchiveAsync(DummyContainerName, true, DummyObjectName, true, stream, format, cancellationToken, progress);
        }

        /// <summary>
        /// Upload an archive and extract the contained files to create objects in the Object Storage Service.
        /// The <paramref name="container"/> argument specifies the container into which the extracted objects
        /// are placed.
        /// </summary>
        /// <param name="service">The <see cref="IObjectStorageService"/> instance.</param>
        /// <param name="container">The name of the container to place the extracted files in.</param>
        /// <param name="stream">A stream providing the raw data of the archive to upload and extract.</param>
        /// <param name="format">An <see cref="ArchiveFormat"/> instance describing the file format of the data in <paramref name="stream"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <param name="progress">An <see cref="IProgress{T}"/> instance to notify about progress of sending data to the Object Storage Service, or <see langword="null"/> if progress updates are not required.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task completes
        /// successfully, the <see cref="Task{TResult}.Result"/> property contains an
        /// <see cref="ExtractArchiveResponse"/> instance describing the results of the Extract
        /// Archive operation.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="container"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="stream"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="format"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/archive-auto-extract.html">Auto-extract archive files (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<ExtractArchiveResponse> ExtractArchiveAsync(this IObjectStorageService service, ContainerName container, Stream stream, ArchiveFormat format, CancellationToken cancellationToken, IProgress<long> progress)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return service.ExtractArchiveAsync(container, false, DummyObjectName, true, stream, format, cancellationToken, progress);
        }

        /// <summary>
        /// Upload an archive and extract the contained files to create objects in the Object Storage Service.
        /// </summary>
        /// <param name="service">The <see cref="IObjectStorageService"/> instance.</param>
        /// <param name="container">The name of the container to place the extracted files in.</param>
        /// <param name="objectPrefix">The object name prefix to apply to extracted files as they are extracted into the container.</param>
        /// <param name="stream">A stream providing the raw data of the archive to upload and extract.</param>
        /// <param name="format">An <see cref="ArchiveFormat"/> instance describing the file format of the data in <paramref name="stream"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <param name="progress">An <see cref="IProgress{T}"/> instance to notify about progress of sending data to the Object Storage Service, or <see langword="null"/> if progress updates are not required.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task completes
        /// successfully, the <see cref="Task{TResult}.Result"/> property contains an
        /// <see cref="ExtractArchiveResponse"/> instance describing the results of the Extract
        /// Archive operation.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="container"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="objectPrefix"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="stream"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="format"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/archive-auto-extract.html">Auto-extract archive files (OpenStack Object Storage API V1 Reference)</seealso>
        public static Task<ExtractArchiveResponse> ExtractArchiveAsync(this IObjectStorageService service, ContainerName container, ObjectName objectPrefix, Stream stream, ArchiveFormat format, CancellationToken cancellationToken, IProgress<long> progress)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return service.ExtractArchiveAsync(container, false, objectPrefix, false, stream, format, cancellationToken, progress);
        }

        /// <summary>
        /// This method implements general support for the multiple forms an Extract Archive operation can take.
        /// </summary>
        /// <param name="service">The <see cref="IObjectStorageService"/> instance.</param>
        /// <param name="container">The name of the container to place the extracted files in, or a placeholder container name if <paramref name="removeContainer"/> is <see langword="true"/>.</param>
        /// <param name="removeContainer"><see langword="true"/> if <paramref name="container"/> is a placeholder container name that should be removed from the final URI before sending the request; otherwise, <see langword="false"/>.</param>
        /// <param name="objectPrefix">The prefix to apply to extracted objects, or a placeholder object prefix if <paramref name="removeObject"/> is <see langword="true"/>.</param>
        /// <param name="removeObject"><see langword="true"/> if <paramref name="objectPrefix"/> is a placeholder object name that should be removed from the final URI before sending the request; otherwise, <see langword="false"/>.</param>
        /// <param name="stream">A stream providing the raw data of the archive to upload and extract.</param>
        /// <param name="format">An <see cref="ArchiveFormat"/> instance describing the file format of the data in <paramref name="stream"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <param name="progress">An <see cref="IProgress{T}"/> instance to notify about progress of sending data to the Object Storage Service, or <see langword="null"/> if progress updates are not required.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task completes
        /// successfully, the <see cref="Task{TResult}.Result"/> property contains an
        /// <see cref="ExtractArchiveResponse"/> instance describing the results of the Extract
        /// Archive operation.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="container"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="objectPrefix"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="stream"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="format"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// If <paramref name="removeContainer"/> is <see langword="true"/> but <paramref name="removeObject"/> is <see langword="false"/>.
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        private static Task<ExtractArchiveResponse> ExtractArchiveAsync(this IObjectStorageService service, ContainerName container, bool removeContainer, ObjectName objectPrefix, bool removeObject, Stream stream, ArchiveFormat format, CancellationToken cancellationToken, IProgress<long> progress)
        {
            if (service == null)
                throw new ArgumentNullException("service");
            if (container == null)
                throw new ArgumentNullException("container");
            if (objectPrefix == null)
                throw new ArgumentNullException("objectPrefix");
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (format == null)
                throw new ArgumentNullException("format");
            if (removeContainer && !removeObject)
                throw new InvalidOperationException("removeObject must be true if removeContainer is true");

            return
                CoreTaskExtensions.Using(
                    () => service.PrepareCreateObjectAsync(container, objectPrefix, stream, cancellationToken, progress),
                    task =>
                    {
                        task.Result.RequestMessage.RequestUri = ModifyUri(task.Result.RequestMessage.RequestUri, format, removeContainer, removeObject);
                        Func<Task<Tuple<HttpResponseMessage, string>>, ExtractArchiveResponse> deserialize = DeserializeExtractArchiveResponse;
                        return task.Result.SendAsync(cancellationToken).Select(deserialize);
                    });
        }

        /// <summary>
        /// Deserialize the body of the response to the HTTP API call as a JSON-encoded
        /// <see cref="ExtractArchiveResponse"/> instance.
        /// </summary>
        /// <param name="task">A <see cref="Task"/> representing the result of an asynchronous operation to upload and extract an archive.</param>
        /// <returns>
        /// An <see cref="ExtractArchiveResponse"/> instance representing the result of the HTTP API call.
        /// <para>-or-</para>
        /// <para><see langword="null"/>, if the <see cref="HttpContentHeaders.ContentType"/> header of the response is not acceptable according to the <see cref="HttpRequestHeaders.Accept"/> header of the request.</para>
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        private static ExtractArchiveResponse DeserializeExtractArchiveResponse(Task<Tuple<HttpResponseMessage, string>> task)
        {
            if (task == null)
                throw new ArgumentNullException("task");

            if (!HttpApiCall.IsAcceptable(task.Result.Item1))
                return null;

            return JsonConvert.DeserializeObject<ExtractArchiveResponse>(task.Result.Item2);
        }

        /// <summary>
        /// Transforms a URI originally created by <see cref="IObjectStorageService.PrepareCreateObjectAsync"/> into
        /// the form required by the Extract Archive operation.
        /// </summary>
        /// <param name="originalUri">The original URI created by <see cref="IObjectStorageService.PrepareCreateObjectAsync"/>.</param>
        /// <param name="format">An <see cref="ArchiveFormat"/> instance describing the format of the archive being uploaded.</param>
        /// <param name="removeContainer"><see langword="true"/> to remove the container name from the URI; otherwise, <see langword="false"/>. For additional information, see the reference documentation for the Extract Archive operation.</param>
        /// <param name="removeObject"><see langword="true"/> to remove the object name from the URI; otherwise, <see langword="false"/>. For additional information, see the reference documentation for the Extract Archive operation.</param>
        /// <returns>The modified URI.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="originalUri"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="format"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// If <paramref name="removeContainer"/> is <see langword="true"/> but <paramref name="removeObject"/> is <see langword="false"/>.
        /// </exception>
        private static Uri ModifyUri(Uri originalUri, ArchiveFormat format, bool removeContainer, bool removeObject)
        {
            if (originalUri == null)
                throw new ArgumentNullException("originalUri");
            if (format == null)
                throw new ArgumentNullException("format");
            if (removeContainer && !removeObject)
                throw new InvalidOperationException("removeObject must be true if removeContainer is true");

            string originalString = originalUri.OriginalString;
            if (removeObject)
                originalString = ReplaceLastInstance(originalString, "/" + DummyObjectName.Value, string.Empty);
            if (removeContainer)
                originalString = ReplaceLastInstance(originalString, "/" + DummyContainerName.Value, string.Empty);

            Uri uri = new Uri(originalString, originalUri.IsAbsoluteUri ? UriKind.Absolute : UriKind.Relative);
            return UriUtility.SetQueryParameter(uri, "extract-archive", format.Name);
        }

        /// <summary>
        /// Replaces the last instance of <paramref name="oldValue"/> in the input string
        /// <paramref name="s"/>, if any exists, with the new value <paramref name="newValue"/>.
        /// </summary>
        /// <param name="s">The original string.</param>
        /// <param name="oldValue">The string to search for.</param>
        /// <param name="newValue">The string to replace the last instance of <paramref name="oldValue"/> with.</param>
        /// <returns>
        /// A string which is a copy of <paramref name="s"/> with the last instance of <paramref name="oldValue"/> replaced with <paramref name="newValue"/>.
        /// <para>-or-</para>
        /// <para><paramref name="s"/>, if it does not contain the substring <paramref name="oldValue"/>.</para>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="s"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="oldValue"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="newValue"/> is <see langword="null"/>.</para>
        /// </exception>
        private static string ReplaceLastInstance(string s, string oldValue, string newValue)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (oldValue == null)
                throw new ArgumentNullException("oldValue");
            if (newValue == null)
                throw new ArgumentNullException("newValue");

            int index = s.LastIndexOf(oldValue);
            if (index < 0)
                return s;

            string prefix = s.Substring(0, index);
            string suffix = s.Substring(index + oldValue.Length);
            return prefix + newValue + suffix;
        }
    }
}
