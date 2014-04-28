namespace OpenStack.Services.ObjectStorage.V1
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.Collections;
    using OpenStack.Net;
    using OpenStack.Security.Authentication;
    using Rackspace.Net;
    using Rackspace.Threading;
    using Stream = System.IO.Stream;

#if !NET40PLUS
    using OpenStack.Compat;
#endif

    public class ObjectStorageClient : ServiceClient, IObjectStorageService
    {
        /// <summary>
        /// Specifies whether the <see cref="Endpoint.PublicURL"/> or <see cref="Endpoint.InternalURL"/>
        /// should be used for accessing the Cloud Queues API.
        /// </summary>
        private readonly bool _internalUrl;

        /// <summary>
        /// This field caches the base URI used for accessing the Cloud Queues service.
        /// </summary>
        /// <seealso cref="GetBaseUriAsync"/>
        private Uri _baseUri;

        public ObjectStorageClient(IAuthenticationService authenticationService, string defaultRegion, bool internalUrl)
            : base(authenticationService, defaultRegion)
        {
            _internalUrl = internalUrl;
        }

        #region IObjectStorageService Members

        public Task<HttpApiCall<ReadOnlyDictionary<string, JObject>>> PrepareGetObjectStorageInfoAsync(CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("/info");
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => CreateJsonApiCall<ReadOnlyDictionary<string, JObject>>(task.Result));
        }

        public Task<HttpApiCall<Tuple<AccountMetadata, ReadOnlyCollectionPage<Container>>>> PrepareListContainersAsync(int? pageSize, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("{?limit}");
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            if (pageSize != null)
                parameters.Add("limit", pageSize.ToString());

            Func<HttpResponseMessage, CancellationToken, Task<Tuple<AccountMetadata, ReadOnlyCollectionPage<Container>>>> deserializeResult =
                (responseMessage, innerCancellationToken) =>
                {
                    AccountMetadata metadata = new AccountMetadata(responseMessage);
                    if (!HttpApiCall.IsAcceptable(responseMessage))
                        throw new HttpWebException(responseMessage);

                    return responseMessage.Content.ReadAsStringAsync()
                        .Select(
                            innerTask =>
                            {
                                IList<Container> list = JsonConvert.DeserializeObject<Container[]>(innerTask.Result);
                                Func<CancellationToken, Task<ReadOnlyCollectionPage<Container>>> getNextPageAsync;
                                if (list.Count > 0)
                                {
                                    getNextPageAsync =
                                        innerCancellationToken2 =>
                                        {
                                            return PrepareListContainersAsync(pageSize, innerCancellationToken2)
                                                .Select(
                                                    _ =>
                                                    {
                                                        bool hasQuery = !string.IsNullOrEmpty(_.Result.RequestMessage.RequestUri.Query);
                                                        UriTemplate nextPageTemplate = new UriTemplate(hasQuery ? "{&marker}" : "{?marker}");
                                                        Uri boundTemplate = nextPageTemplate.BindByName(new Dictionary<string, string> { { "marker", list[list.Count - 1].Name.Value } });
                                                        _.Result.RequestMessage.RequestUri = new Uri(_.Result.RequestMessage.RequestUri.AbsoluteUri + boundTemplate.OriginalString);
                                                        return _.Result;
                                                    })
                                                .Then(
                                                    _ => _.Result.SendAsync(innerCancellationToken2))
                                                .Select(_ => _.Result.Item2.Item2);
                                        };
                                }
                                else
                                {
                                    getNextPageAsync = null;
                                }

                                ReadOnlyCollectionPage<Container> results = new BasicReadOnlyCollectionPage<Container>(list, getNextPageAsync);
                                return Tuple.Create(metadata, results);
                            });
                };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult));
        }

        public Task<HttpApiCall<AccountMetadata>> PrepareGetAccountMetadataAsync(CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate(string.Empty);
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            Func<HttpResponseMessage, CancellationToken, Task<AccountMetadata>> deserializeResult =
                (responseMessage, innerCancellationToken) => CompletedTask.FromResult(new AccountMetadata(responseMessage));

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Head, template, parameters, cancellationToken))
                .Select(task => CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult));
        }

        public Task<HttpApiCall> PrepareUpdateAccountMetadataAsync(AccountMetadata metadata, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate(string.Empty);
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, cancellationToken))
                .Select(
                    task =>
                    {
                        var call = CreateBasicApiCall(task.Result);
                        var requestHeaders = call.RequestMessage.Headers;
                        foreach (var pair in metadata.Headers)
                        {
                            requestHeaders.Remove(pair.Key);
                            requestHeaders.Add(pair.Key, pair.Value);
                        }

                        foreach (var pair in metadata.Metadata)
                        {
                            requestHeaders.Remove(AccountMetadata.AccountMetadataPrefix + pair.Key);
                            requestHeaders.Add(AccountMetadata.AccountMetadataPrefix + pair.Key, pair.Value);
                        }

                        return call;
                    });
        }

        public Task<HttpApiCall> PrepareRemoveAccountMetadataAsync(IEnumerable<string> keys, CancellationToken cancellationToken)
        {
            if (keys == null)
                throw new ArgumentNullException("keys");

            Dictionary<string, string> metadata = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var key in keys)
            {
                if (string.IsNullOrEmpty(key))
                    throw new ArgumentException("keys cannot contain any null or empty values");

                metadata.Add(key, string.Empty);
            }

            return PrepareUpdateAccountMetadataAsync(new AccountMetadata(new Dictionary<string, string>(), metadata), cancellationToken);
        }

        public Task<HttpApiCall> PrepareCreateContainerAsync(ContainerName container, CancellationToken cancellationToken)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            UriTemplate template = new UriTemplate("{container}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "container", container.Value } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Put, template, parameters, cancellationToken))
                .Select(task => CreateBasicApiCall(task.Result));
        }

        public Task<HttpApiCall> PrepareRemoveContainerAsync(ContainerName container, CancellationToken cancellationToken)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            UriTemplate template = new UriTemplate("{container}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "container", container.Value } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => CreateBasicApiCall(task.Result));
        }

        public Task<HttpApiCall<Tuple<ContainerMetadata, ReadOnlyCollectionPage<ContainerObject>>>> PrepareListObjectsAsync(ContainerName container, int? pageSize, CancellationToken cancellationToken)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            UriTemplate template = new UriTemplate("{container}{?limit}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "container", container.Value } };
            if (pageSize != null)
                parameters.Add("limit", pageSize.ToString());

            Func<HttpResponseMessage, CancellationToken, Task<Tuple<ContainerMetadata, ReadOnlyCollectionPage<ContainerObject>>>> deserializeResult =
                (responseMessage, innerCancellationToken) =>
                {
                    ContainerMetadata metadata = new ContainerMetadata(responseMessage);
                    if (!HttpApiCall.IsAcceptable(responseMessage))
                        throw new HttpWebException(responseMessage);

                    return responseMessage.Content.ReadAsStringAsync()
                        .Select(
                            innerTask =>
                            {
                                IList<ContainerObject> list = JsonConvert.DeserializeObject<ContainerObject[]>(innerTask.Result);
                                Func<CancellationToken, Task<ReadOnlyCollectionPage<ContainerObject>>> getNextPageAsync;
                                if (list.Count > 0)
                                {
                                    getNextPageAsync =
                                        innerCancellationToken2 =>
                                        {
                                            return PrepareListObjectsAsync(container, pageSize, innerCancellationToken2)
                                                .Select(
                                                    _ =>
                                                    {
                                                        bool hasQuery = !string.IsNullOrEmpty(_.Result.RequestMessage.RequestUri.Query);
                                                        UriTemplate nextPageTemplate = new UriTemplate(hasQuery ? "{&marker}" : "{?marker}");
                                                        Uri boundTemplate = nextPageTemplate.BindByName(new Dictionary<string, string> { { "marker", list[list.Count - 1].Name.Value } });
                                                        _.Result.RequestMessage.RequestUri = new Uri(_.Result.RequestMessage.RequestUri.AbsoluteUri + boundTemplate.OriginalString);
                                                        return _.Result;
                                                    })
                                                .Then(
                                                    _ => _.Result.SendAsync(innerCancellationToken2))
                                                .Select(_ => _.Result.Item2.Item2);
                                        };
                                }
                                else
                                {
                                    getNextPageAsync = null;
                                }

                                ReadOnlyCollectionPage<ContainerObject> results = new BasicReadOnlyCollectionPage<ContainerObject>(list, getNextPageAsync);
                                return Tuple.Create(metadata, results);
                            });
                };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult));
        }

        public Task<HttpApiCall<ContainerMetadata>> PrepareGetContainerMetadataAsync(ContainerName container, CancellationToken cancellationToken)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            UriTemplate template = new UriTemplate("{container}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "container", container.Value } };

            Func<HttpResponseMessage, CancellationToken, Task<ContainerMetadata>> deserializeResult =
                (responseMessage, innerCancellationToken) => CompletedTask.FromResult(new ContainerMetadata(responseMessage));

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Head, template, parameters, cancellationToken))
                .Select(task => CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult));
        }

        public Task<HttpApiCall> PrepareUpdateContainerMetadataAsync(ContainerName container, ContainerMetadata metadata, CancellationToken cancellationToken)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            UriTemplate template = new UriTemplate("{container}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "container", container.Value } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, cancellationToken))
                .Select(
                    task =>
                    {
                        var call = CreateBasicApiCall(task.Result);
                        var requestHeaders = call.RequestMessage.Headers;
                        foreach (var pair in metadata.Headers)
                        {
                            requestHeaders.Remove(pair.Key);
                            requestHeaders.Add(pair.Key, pair.Value);
                        }

                        foreach (var pair in metadata.Metadata)
                        {
                            requestHeaders.Remove(ContainerMetadata.ContainerMetadataPrefix + pair.Key);
                            requestHeaders.Add(ContainerMetadata.ContainerMetadataPrefix + pair.Key, pair.Value);
                        }

                        return call;
                    });
        }

        public Task<HttpApiCall> PrepareRemoveContainerMetadataAsync(ContainerName container, IEnumerable<string> keys, CancellationToken cancellationToken)
        {
            if (keys == null)
                throw new ArgumentNullException("keys");

            Dictionary<string, string> metadata = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var key in keys)
            {
                if (string.IsNullOrEmpty(key))
                    throw new ArgumentException("keys cannot contain any null or empty values");

                metadata.Add(key, string.Empty);
            }

            return PrepareUpdateContainerMetadataAsync(container, new ContainerMetadata(new Dictionary<string, string>(), metadata), cancellationToken);
        }

        public Task<HttpApiCall> PrepareCreateObjectAsync(ContainerName container, ObjectName @object, Stream stream, CancellationToken cancellationToken, IProgress<long> progress)
        {
            if (container == null)
                throw new ArgumentNullException("container");
            if (@object == null)
                throw new ArgumentNullException("object");
            if (stream == null)
                throw new ArgumentNullException("stream");

            UriTemplate template = new UriTemplate("{container}/{object}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "container", container.Value }, { "object", @object.Value } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Put, template, parameters, cancellationToken))
                .Select(
                    task =>
                    {
                        var call = CreateBasicApiCall(task.Result);
                        call.RequestMessage.Content = new StreamContent(stream);
                        return call;
                    });
        }

        public Task<HttpApiCall> PrepareCopyObjectAsync(ContainerName sourceContainer, ObjectName sourceObject, ContainerName destinationContainer, ObjectName destinationObject, CancellationToken cancellationToken)
        {
            if (sourceContainer == null)
                throw new ArgumentNullException("sourceContainer");
            if (sourceObject == null)
                throw new ArgumentNullException("sourceObject");
            if (destinationContainer == null)
                throw new ArgumentNullException("destinationContainer");
            if (destinationObject == null)
                throw new ArgumentNullException("destinationObject");

            UriTemplate template = new UriTemplate("{container}/{object}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "container", sourceContainer.Value }, { "object", sourceObject.Value } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(new HttpMethod("COPY"), template, parameters, cancellationToken))
                .Select(
                    task =>
                    {
                        var call = CreateBasicApiCall(task.Result);
                        var requestHeaders = call.RequestMessage.Headers;

                        UriTemplate destinationTemplate = new UriTemplate("/{container}/{object}");
                        Dictionary<string, string> destinationParameters = new Dictionary<string, string>
                        {
                            { "container", destinationContainer.Value },
                            { "object", destinationObject.Value }
                        };

                        requestHeaders.Add("Destination", destinationTemplate.BindByName(destinationParameters).OriginalString);
                        return call;
                    });
        }

        public Task<HttpApiCall> PrepareRemoveObjectAsync(ContainerName container, ObjectName @object, CancellationToken cancellationToken)
        {
            if (container == null)
                throw new ArgumentNullException("container");
            if (@object == null)
                throw new ArgumentNullException("object");

            UriTemplate template = new UriTemplate("{container}/{object}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "container", container.Value }, { "object", @object.Value } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => CreateBasicApiCall(task.Result));
        }

        public Task<HttpApiCall<Tuple<ObjectMetadata, Stream>>> PrepareGetObjectAsync(ContainerName container, ObjectName @object, CancellationToken cancellationToken)
        {
            if (container == null)
                throw new ArgumentNullException("container");
            if (@object == null)
                throw new ArgumentNullException("object");

            UriTemplate template = new UriTemplate("{container}/{object}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "container", container.Value }, { "object", @object.Value } };

            Func<HttpResponseMessage, CancellationToken, Task<Tuple<ObjectMetadata, Stream>>> deserializeResult =
                (responseMessage, innerCancellationToken) =>
                {
                    return responseMessage.Content.ReadAsStreamAsync()
                        .Select(innerTask => Tuple.Create(new ObjectMetadata(responseMessage), innerTask.Result));
                };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseHeadersRead, deserializeResult));
        }

        public Task<HttpApiCall<ObjectMetadata>> PrepareGetObjectMetadataAsync(ContainerName container, ObjectName @object, CancellationToken cancellationToken)
        {
            if (container == null)
                throw new ArgumentNullException("container");
            if (@object == null)
                throw new ArgumentNullException("object");

            UriTemplate template = new UriTemplate("{container}/{object}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "container", container.Value }, { "object", @object.Value } };

            Func<HttpResponseMessage, CancellationToken, Task<ObjectMetadata>> deserializeResult =
                (responseMessage, innerCancellationToken) => CompletedTask.FromResult(new ObjectMetadata(responseMessage));

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Head, template, parameters, cancellationToken))
                .Select(task => CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult));
        }

        public Task<HttpApiCall> PrepareUpdateObjectMetadataAsync(ContainerName container, ObjectName @object, ObjectMetadata metadata, CancellationToken cancellationToken)
        {
            if (metadata == null)
                throw new ArgumentNullException("metadata");

            return PrepareCopyObjectAsync(container, @object, container, @object, cancellationToken)
                .Select(
                    task =>
                    {
                        var requestHeaders = task.Result.RequestMessage.Headers;
                        foreach (var pair in metadata.Headers)
                        {
                            requestHeaders.Remove(pair.Key);
                            requestHeaders.Add(pair.Key, pair.Value);
                        }

                        foreach (var pair in metadata.Metadata)
                        {
                            requestHeaders.Remove(ObjectMetadata.ObjectMetadataPrefix + pair.Key);
                            requestHeaders.Add(ObjectMetadata.ObjectMetadataPrefix + pair.Key, pair.Value);
                        }

                        return task.Result;
                    });
        }

        public Task<HttpApiCall> PrepareRemoveObjectMetadataAsync(ContainerName container, ObjectName @object, IEnumerable<string> keys, CancellationToken cancellationToken)
        {
            if (keys == null)
                throw new ArgumentNullException("keys");

            Dictionary<string, string> metadata = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var key in keys)
            {
                if (string.IsNullOrEmpty(key))
                    throw new ArgumentException("keys cannot contain any null or empty values");

                metadata.Add(key, string.Empty);
            }

            return PrepareUpdateObjectMetadataAsync(container, @object, new ObjectMetadata(new Dictionary<string, string>(), metadata), cancellationToken);
        }

        #endregion

        /// <inheritdoc/>
        /// <remarks>
        /// This method returns a cached base address if one is available. If no cached address is
        /// available, <see cref="ProviderBase{TProvider}.GetServiceEndpoint"/> is called to obtain
        /// an <see cref="Endpoint"/> with the type <c>??</c> and preferred type <c>??</c>.
        /// </remarks>
        protected override Task<Uri> GetBaseUriAsync(CancellationToken cancellationToken)
        {
            if (_baseUri != null)
            {
                return CompletedTask.FromResult(_baseUri);
            }

            const string serviceType = "object-store";
            const string serviceName = null;
            return AuthenticationService.GetBaseAddressAsync(serviceType, serviceName, DefaultRegion, _internalUrl, cancellationToken)
                .Select(
                    task =>
                    {
                        _baseUri = task.Result;
                        return task.Result;
                    });
        }
    }
}
