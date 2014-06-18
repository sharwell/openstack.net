﻿namespace OpenStack.Services.ObjectStorage.V1
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.Collections;
    using OpenStack.IO;
    using OpenStack.Net;
    using OpenStack.Security.Authentication;
    using Rackspace.Net;
    using Rackspace.Threading;
    using Stream = System.IO.Stream;

#if !NET40PLUS
    using OpenStack.Compat;
#endif

    /// <summary>
    /// This class provides a default implementation of <see cref="IObjectStorageService"/> suitable for
    /// connecting to OpenStack-compatible installations of the Object Storage Service V1.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class ObjectStorageClient : ServiceClient, IObjectStorageService
    {
        /// <summary>
        /// Specifies whether the public or internal base address
        /// should be used for accessing the object storage service.
        /// </summary>
        private readonly bool _internalUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectStorageClient"/> class
        /// with the specified authentication service, default region, and value indicating
        /// whether an internal or public endpoint should be used for communicating with
        /// the service.
        /// </summary>
        /// <param name="authenticationService">The authentication service to use for authenticating requests made to this service.</param>
        /// <param name="defaultRegion">The preferred region for the service. Unless otherwise specified for a specific client, derived service clients will not use a default region if this value is <see langword="null"/> (i.e. only regionless or global service endpoints will be considered acceptable).</param>
        /// <param name="internalUrl"><see langword="true"/> to access the service over a local network; otherwise, <see langword="false"/> to access the service over a public network (the internet).</param>
        /// <exception cref="ArgumentNullException">If <paramref name="authenticationService"/> is <see langword="null"/>.</exception>
        public ObjectStorageClient(IAuthenticationService authenticationService, string defaultRegion, bool internalUrl)
            : base(authenticationService, defaultRegion)
        {
            _internalUrl = internalUrl;
        }

        #region IObjectStorageService Members

        /// <inheritdoc/>
        public Task<GetObjectStorageInfoApiCall> PrepareGetObjectStorageInfoAsync(CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("/info");
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetObjectStorageInfoApiCall(CreateJsonApiCall<ReadOnlyDictionary<string, JObject>>(task.Result)));
        }

        /// <inheritdoc/>
        public Task<ListContainersApiCall> PrepareListContainersAsync(CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate(string.Empty);
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            Func<HttpResponseMessage, CancellationToken, Task<Tuple<AccountMetadata, ReadOnlyCollectionPage<Container>>>> deserializeResult =
                (responseMessage, innerCancellationToken) =>
                {
                    Uri originalUri = responseMessage.RequestMessage.RequestUri;

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
                                            ContainerName marker = list.Last().Name;
                                            return
                                                CoreTaskExtensions.Using(
                                                    () => PrepareListContainersAsync(innerCancellationToken2)
                                                        .Select(
                                                            _ =>
                                                            {
                                                                _.Result.RequestMessage.RequestUri = originalUri;
                                                                return _.Result;
                                                            })
                                                        .WithMarker(marker),
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
                .Select(task => new ListContainersApiCall(CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        /// <inheritdoc/>
        public Task<GetAccountMetadataApiCall> PrepareGetAccountMetadataAsync(CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate(string.Empty);
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            Func<HttpResponseMessage, CancellationToken, Task<AccountMetadata>> deserializeResult =
                (responseMessage, innerCancellationToken) => CompletedTask.FromResult(new AccountMetadata(responseMessage));

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Head, template, parameters, cancellationToken))
                .Select(task => new GetAccountMetadataApiCall(CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        /// <inheritdoc/>
        public Task<UpdateAccountMetadataApiCall> PrepareUpdateAccountMetadataAsync(AccountMetadata metadata, CancellationToken cancellationToken)
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
                            requestHeaders.Add(pair.Key, StorageMetadata.EncodeHeaderValue(pair.Value));
                        }

                        foreach (var pair in metadata.Metadata)
                        {
                            requestHeaders.Remove(AccountMetadata.AccountMetadataPrefix + pair.Key);
                            requestHeaders.Add(AccountMetadata.AccountMetadataPrefix + pair.Key, StorageMetadata.EncodeHeaderValue(pair.Value));
                        }

                        return new UpdateAccountMetadataApiCall(call);
                    });
        }

        /// <inheritdoc/>
        public Task<UpdateAccountMetadataApiCall> PrepareRemoveAccountMetadataAsync(IEnumerable<string> keys, CancellationToken cancellationToken)
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

        /// <inheritdoc/>
        public Task<CreateContainerApiCall> PrepareCreateContainerAsync(ContainerName container, CancellationToken cancellationToken)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            UriTemplate template = new UriTemplate("{container}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "container", container.Value } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Put, template, parameters, cancellationToken))
                .Select(task => new CreateContainerApiCall(CreateBasicApiCall(task.Result)));
        }

        /// <inheritdoc/>
        public Task<RemoveContainerApiCall> PrepareRemoveContainerAsync(ContainerName container, CancellationToken cancellationToken)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            UriTemplate template = new UriTemplate("{container}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "container", container.Value } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => new RemoveContainerApiCall(CreateBasicApiCall(task.Result)));
        }

        /// <inheritdoc/>
        public Task<ListObjectsApiCall> PrepareListObjectsAsync(ContainerName container, CancellationToken cancellationToken)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            UriTemplate template = new UriTemplate("{container}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "container", container.Value } };

            Func<HttpResponseMessage, CancellationToken, Task<Tuple<ContainerMetadata, ReadOnlyCollectionPage<ContainerObject>>>> deserializeResult =
                (responseMessage, innerCancellationToken) =>
                {
                    Uri originalUri = responseMessage.RequestMessage.RequestUri;

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
                                            ObjectName marker = list.Last().Name;
                                            return
                                                CoreTaskExtensions.Using(
                                                    () => PrepareListObjectsAsync(container, innerCancellationToken2)
                                                        .Select(
                                                            _ =>
                                                            {
                                                                _.Result.RequestMessage.RequestUri = originalUri;
                                                                return _.Result;
                                                            })
                                                        .WithMarker(marker),
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
                .Select(task => new ListObjectsApiCall(CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        /// <inheritdoc/>
        public Task<GetContainerMetadataApiCall> PrepareGetContainerMetadataAsync(ContainerName container, CancellationToken cancellationToken)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            UriTemplate template = new UriTemplate("{container}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "container", container.Value } };

            Func<HttpResponseMessage, CancellationToken, Task<ContainerMetadata>> deserializeResult =
                (responseMessage, innerCancellationToken) => CompletedTask.FromResult(new ContainerMetadata(responseMessage));

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Head, template, parameters, cancellationToken))
                .Select(task => new GetContainerMetadataApiCall(CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        /// <inheritdoc/>
        public Task<UpdateContainerMetadataApiCall> PrepareUpdateContainerMetadataAsync(ContainerName container, ContainerMetadata metadata, CancellationToken cancellationToken)
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
                            requestHeaders.Add(pair.Key, StorageMetadata.EncodeHeaderValue(pair.Value));
                        }

                        foreach (var pair in metadata.Metadata)
                        {
                            requestHeaders.Remove(ContainerMetadata.ContainerMetadataPrefix + pair.Key);
                            requestHeaders.Add(ContainerMetadata.ContainerMetadataPrefix + pair.Key, StorageMetadata.EncodeHeaderValue(pair.Value));
                        }

                        return new UpdateContainerMetadataApiCall(call);
                    });
        }

        /// <inheritdoc/>
        public Task<UpdateContainerMetadataApiCall> PrepareRemoveContainerMetadataAsync(ContainerName container, IEnumerable<string> keys, CancellationToken cancellationToken)
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

        /// <inheritdoc/>
        public Task<CreateObjectApiCall> PrepareCreateObjectAsync(ContainerName container, ObjectName @object, Stream stream, CancellationToken cancellationToken, IProgress<long> progress)
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
                        Stream contentStream = stream;
                        if (progress != null)
                            contentStream = new ProgressStream(contentStream, progress);

                        call.RequestMessage.Content = new StreamContent(contentStream);
                        return new CreateObjectApiCall(call);
                    });
        }

        /// <inheritdoc/>
        public Task<CopyObjectApiCall> PrepareCopyObjectAsync(ContainerName sourceContainer, ObjectName sourceObject, ContainerName destinationContainer, ObjectName destinationObject, CancellationToken cancellationToken)
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
                        return new CopyObjectApiCall(call);
                    });
        }

        /// <inheritdoc/>
        public Task<RemoveObjectApiCall> PrepareRemoveObjectAsync(ContainerName container, ObjectName @object, CancellationToken cancellationToken)
        {
            if (container == null)
                throw new ArgumentNullException("container");
            if (@object == null)
                throw new ArgumentNullException("object");

            UriTemplate template = new UriTemplate("{container}/{object}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "container", container.Value }, { "object", @object.Value } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => new RemoveObjectApiCall(CreateBasicApiCall(task.Result)));
        }

        /// <inheritdoc/>
        public Task<GetObjectApiCall> PrepareGetObjectAsync(ContainerName container, ObjectName @object, CancellationToken cancellationToken)
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
                .Select(task => new GetObjectApiCall(CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseHeadersRead, deserializeResult)));
        }

        /// <inheritdoc/>
        public Task<GetObjectMetadataApiCall> PrepareGetObjectMetadataAsync(ContainerName container, ObjectName @object, CancellationToken cancellationToken)
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
                .Select(task => new GetObjectMetadataApiCall(CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        /// <inheritdoc/>
        /// <remarks>
        /// This implementation avoids sending HTTP headers present in the <see cref="StorageMetadata.Headers"/> property
        /// of <paramref name="metadata"/> which the Object Storage service is known to reject. This simplifies the use
        /// of this method in combination with <see cref="PrepareGetObjectMetadataAsync"/> to update, as opposed to replace,
        /// the metadata associated with an object.
        /// </remarks>
        public Task<SetObjectMetadataApiCall> PrepareSetObjectMetadataAsync(ContainerName container, ObjectName @object, ObjectMetadata metadata, CancellationToken cancellationToken)
        {
            if (container == null)
                throw new ArgumentNullException("container");
            if (@object == null)
                throw new ArgumentNullException("object");
            if (metadata == null)
                throw new ArgumentNullException("metadata");

            UriTemplate template = new UriTemplate("{container}/{object}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "container", container.Value }, { "object", @object.Value } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, cancellationToken))
                .Select(
                    task =>
                    {
                        var call = CreateBasicApiCall(task.Result);
                        if (call.RequestMessage.Content == null)
                            call.RequestMessage.Content = new ByteArrayContent(new byte[0]);
                        var requestHeaders = call.RequestMessage.Headers;
                        var contentHeaders = call.RequestMessage.Content.Headers;
                        foreach (var pair in metadata.Headers)
                        {
                            switch (pair.Key.ToLowerInvariant())
                            {
                            case "content-length":
                            case "etag":
                            case "accept-ranges":
                            //case "x-trans-id":
                            case "x-timestamp":
                            case "date":
                                continue;

                            case "allow":
                            case "last-modified":
                                //contentHeaders.Remove(pair.Key);
                                //contentHeaders.Add(pair.Key, pair.Value);
                                break;

                            default:
                                if (pair.Key.ToLowerInvariant().StartsWith("content-"))
                                {
                                    contentHeaders.Remove(pair.Key);
                                    if (!string.IsNullOrEmpty(pair.Value))
                                        contentHeaders.Add(pair.Key, StorageMetadata.EncodeHeaderValue(pair.Value));
                                }
                                else
                                {
                                    requestHeaders.Remove(pair.Key);
                                    if (!string.IsNullOrEmpty(pair.Value))
                                        requestHeaders.Add(pair.Key, StorageMetadata.EncodeHeaderValue(pair.Value));
                                }

                                break;
                            }
                        }

                        foreach (var pair in metadata.Metadata)
                        {
                            string prefix = ObjectMetadata.ObjectMetadataPrefix;
                            string key = prefix + pair.Key;
                            string value = pair.Value;
                            requestHeaders.Remove(key);
                            if (!string.IsNullOrEmpty(value))
                                requestHeaders.Add(key, StorageMetadata.EncodeHeaderValue(value));
                        }

                        return new SetObjectMetadataApiCall(call);
                    });
        }

        #endregion

        /// <inheritdoc/>
        /// <remarks>
        /// This method calls <see cref="IAuthenticationService.GetBaseAddressAsync"/> to obtain a URI
        /// for the type <c>object-store</c>. The preferred name is not specified.
        /// </remarks>
        protected override Task<Uri> GetBaseUriAsyncImpl(CancellationToken cancellationToken)
        {
            const string serviceType = "object-store";
            const string serviceName = null;
            return AuthenticationService.GetBaseAddressAsync(serviceType, serviceName, DefaultRegion, _internalUrl, cancellationToken);
        }
    }
}
