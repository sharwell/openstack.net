namespace OpenStack.Services.ObjectStorage.V1
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
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

        protected ObjectStorageClient(IAuthenticationService authenticationService, string defaultRegion, bool internalUrl)
            : base(authenticationService, defaultRegion)
        {
            _internalUrl = internalUrl;
        }

        #region IObjectStorageService Members

        public Task<HttpApiCall<ReadOnlyDictionary<string, JObject>>> PrepareGetObjectStorageInfoAsync(CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("info");
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

            //return GetBaseUriAsync(cancellationToken)
            //    .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
            //    .Select(task => CreateJsonApiCall<ReadOnlyDictionary<string, JObject>>(task.Result));
            throw new NotImplementedException();
        }

        public Task<HttpApiCall<AccountMetadata>> PrepareGetAccountMetadataAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<HttpApiCall> PrepareUpdateAccountMetadataAsync(AccountMetadata metadata, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<HttpApiCall> PrepareRemoveAccountMetadataAsync(IEnumerable<string> keys, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<HttpApiCall> PrepareCreateContainerAsync(ContainerName container, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<HttpApiCall> PrepareRemoveContainerAsync(ContainerName container, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<HttpApiCall<Tuple<ContainerMetadata, ReadOnlyCollectionPage<object>>>> PrepareListObjectsAsync(ContainerName container, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<HttpApiCall<ContainerMetadata>> PrepareGetContainerMetadataAsync(ContainerName container, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<HttpApiCall> PrepareUpdateContainerMetadataAsync(ContainerName container, ContainerMetadata metadata, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<HttpApiCall> PrepareRemoveContainerMetadataAsync(ContainerName container, IEnumerable<string> keys, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<HttpApiCall> PrepareCreateObjectAsync(ContainerName container, ObjectName @object, Stream stream, CancellationToken cancellationToken, IProgress<long> progress)
        {
            throw new NotImplementedException();
        }

        public Task<HttpApiCall> PrepareCopyObjectAsync(ContainerName sourceContainer, ObjectName sourceObject, ContainerName destinationContainer, ObjectName destinationObject, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<HttpApiCall> PrepareRemoveObjectAsync(ContainerName container, ObjectName @object, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<HttpApiCall<Tuple<ObjectMetadata, Stream>>> PrepareGetObjectAsync(ContainerName container, ObjectName @object, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<HttpApiCall<ObjectMetadata>> PrepareGetObjectMetadataAsync(ContainerName container, ObjectName @object, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<HttpApiCall> PrepareUpdateObjectMetadataAsync(ContainerName container, ObjectName @object, ObjectMetadata metadata, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<HttpApiCall> PrepareRemoveObjectMetadataAsync(ContainerName container, ObjectName @object, IEnumerable<string> keys, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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
