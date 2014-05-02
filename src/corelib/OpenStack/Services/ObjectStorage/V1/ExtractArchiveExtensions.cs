namespace OpenStack.Services.ObjectStorage.V1
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using OpenStack.Net;
    using Rackspace.Net;
    using Rackspace.Threading;
    using Stream = System.IO.Stream;

#if !NET40PLUS
    using OpenStack.Compat;
#endif

    public static class ExtractArchiveExtensions
    {
        private static readonly ContainerName DummyContainerName = new ContainerName("dummyContainer");

        private static readonly ObjectName DummyObjectName = new ObjectName("dummyObject");

        public static Task<bool> SupportsExtractArchiveAsync(this IObjectStorageService storageService, CancellationToken cancellationToken)
        {
            return storageService.GetObjectStorageInfoAsync(cancellationToken)
                .Select(task => task.Result.ContainsKey("bulk_upload"));
        }

        public static Task<ExtractArchiveResponse> ExtractArchiveAsync(this IObjectStorageService service, Stream stream, ArchiveFormat format, CancellationToken cancellationToken, IProgress<long> progress)
        {
            return service.ExtractArchiveAsync(DummyContainerName, true, DummyObjectName, true, stream, format, cancellationToken, progress);
        }

        public static Task<ExtractArchiveResponse> ExtractArchiveAsync(this IObjectStorageService service, ContainerName container, Stream stream, ArchiveFormat format, CancellationToken cancellationToken, IProgress<long> progress)
        {
            return service.ExtractArchiveAsync(container, false, DummyObjectName, true, stream, format, cancellationToken, progress);
        }

        public static Task<ExtractArchiveResponse> ExtractArchiveAsync(this IObjectStorageService service, ContainerName container, ObjectName objectPrefix, Stream stream, ArchiveFormat format, CancellationToken cancellationToken, IProgress<long> progress)
        {
            return service.ExtractArchiveAsync(container, false, objectPrefix, false, stream, format, cancellationToken, progress);
        }

        private static Task<ExtractArchiveResponse> ExtractArchiveAsync(this IObjectStorageService service, ContainerName container, bool removeContainer, ObjectName objectPrefix, bool removeObject, Stream stream, ArchiveFormat format, CancellationToken cancellationToken, IProgress<long> progress)
        {
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

        private static ExtractArchiveResponse DeserializeExtractArchiveResponse(Task<Tuple<HttpResponseMessage, string>> task)
        {
            if (!HttpApiCall.IsAcceptable(task.Result.Item1))
                return null;

            return JsonConvert.DeserializeObject<ExtractArchiveResponse>(task.Result.Item2);
        }

        private static Uri ModifyUri(Uri originalUri, ArchiveFormat format, bool removeContainer, bool removeObject)
        {
            string originalString = originalUri.OriginalString;
            if (removeObject)
                originalString = ReplaceLastInstance(originalString, "/" + DummyObjectName.Value, string.Empty);
            if (removeContainer)
                originalString = ReplaceLastInstance(originalString, "/" + DummyContainerName.Value, string.Empty);

            bool hasQuery = !string.IsNullOrEmpty(originalUri.Query);
            UriTemplate template = new UriTemplate(hasQuery ? "{&extract%2Darchive}" : "{?extract%2Darchive}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "extract%2Darchive", format.Name } };
            Uri boundUri = template.BindByName(parameters);
            return new Uri(originalString + boundUri.OriginalString, UriKind.Absolute);
        }

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
