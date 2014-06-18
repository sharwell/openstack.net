namespace OpenStack.Services.Compute.V2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

    /// <summary>
    /// This class provides a default implementation of <see cref="IComputeService"/> suitable for
    /// connecting to OpenStack-compatible installations of the Compute Service V2.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class ComputeClient : ServiceClient, IComputeService
    {
        /// <summary>
        /// Specifies whether the public or internal base address
        /// should be used for accessing the compute service.
        /// </summary>
        private readonly bool _internalUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComputeClient"/> class with the
        /// specified authentication service, default region, and value specifying whether
        /// the internal or public URI should be used for communicating with the compute service.
        /// </summary>
        /// <param name="authenticationService">The authentication service to use for authenticating requests made to this service.</param>
        /// <param name="defaultRegion">The preferred region for the service. Unless otherwise specified for a specific client, derived service clients will not use a default region if this value is <see langword="null"/> (i.e. only regionless or global service endpoints will be considered acceptable).</param>
        /// <param name="internalUrl"><see langword="true"/> to access the service over a local network; otherwise, <see langword="false"/> to access the service over a public network (the internet).</param>
        public ComputeClient(IAuthenticationService authenticationService, string defaultRegion, bool internalUrl)
            : base(authenticationService, defaultRegion)
        {
            _internalUrl = internalUrl;
        }

        #region Servers

        /// <inheritdoc/>
        public virtual Task<ListServersApiCall> PrepareListServersAsync(CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("servers");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            Func<HttpResponseMessage, CancellationToken, Task<ReadOnlyCollectionPage<Server>>> deserializeResult =
                (responseMessage, innerCancellationToken) =>
                {
                    Uri originalUri = responseMessage.RequestMessage.RequestUri;

                    if (!HttpApiCall.IsAcceptable(responseMessage))
                        throw new HttpWebException(responseMessage);

                    return responseMessage.Content.ReadAsStringAsync()
                        .Select(
                            innerTask =>
                            {
                                if (string.IsNullOrEmpty(innerTask.Result))
                                    return null;

                                JObject responseObject = JsonConvert.DeserializeObject<JObject>(innerTask.Result);
                                JArray serversArray = responseObject["servers"] as JArray;
                                if (serversArray == null)
                                    return null;

                                IList<Server> list = serversArray.ToObject<Server[]>();
                                Func<CancellationToken, Task<ReadOnlyCollectionPage<Server>>> getNextPageAsync =
                                    CreateGetNextPageAsyncDelegate<ListServersApiCall, Server>(PrepareListServersAsync, responseObject, "servers");

                                ReadOnlyCollectionPage<Server> results = new BasicReadOnlyCollectionPage<Server>(list, getNextPageAsync);
                                return results;
                            });
                };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new ListServersApiCall(CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        /// <inheritdoc/>
        public virtual Task<CreateServerApiCall> PrepareCreateServerAsync(ServerRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("servers");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken))
                .Select(task => new CreateServerApiCall(CreateJsonApiCall<ServerResponse>(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<GetServerApiCall> PrepareGetServerAsync(ServerId serverId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("servers/{server_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "server_id", serverId.Value } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetServerApiCall(CreateJsonApiCall<ServerResponse>(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<UpdateServerApiCall> PrepareUpdateServerAsync(ServerId serverId, ServerRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("servers/{server_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "server_id", serverId.Value } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Put, template, parameters, request, cancellationToken))
                .Select(task => new UpdateServerApiCall(CreateJsonApiCall<ServerResponse>(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<RemoveServerApiCall> PrepareRemoveServerAsync(ServerId serverId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("servers/{server_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "server_id", serverId.Value } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => new RemoveServerApiCall(CreateBasicApiCall(task.Result)));
        }

        #endregion

        #region Server Addresses

        /// <inheritdoc/>
        public virtual Task<GetServerAddressesApiCall> PrepareGetServerAddressesAsync(ServerId serverId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("servers/{server_id}/ips");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "server_id", serverId.Value } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetServerAddressesApiCall(CreateJsonApiCall<AddressesResponse>(task.Result)));
        }

        #endregion

        #region Server Actions

        /// <inheritdoc/>
        public virtual Task<ChangePasswordApiCall> PrepareChangePasswordAsync(ServerId serverId, ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("servers/{server_id}/action");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "server_id", serverId.Value } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken))
                .Select(task => new ChangePasswordApiCall(CreateBasicApiCall(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<RebootServerApiCall> PrepareRebootServerAsync(ServerId serverId, RebootRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("servers/{server_id}/action");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "server_id", serverId.Value } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken))
                .Select(task => new RebootServerApiCall(CreateBasicApiCall(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<RebuildServerApiCall> PrepareRebuildServerAsync(ServerId serverId, RebuildRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("servers/{server_id}/action");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "server_id", serverId.Value } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken))
                .Select(task => new RebuildServerApiCall(CreateJsonApiCall<ServerResponse>(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<ResizeServerApiCall> PrepareResizeServerAsync(ServerId serverId, ResizeRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("servers/{server_id}/action");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "server_id", serverId.Value } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken))
                .Select(task => new ResizeServerApiCall(CreateBasicApiCall(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<ConfirmServerResizeApiCall> PrepareConfirmServerResizeAsync(ServerId serverId, ConfirmServerResizeRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("servers/{server_id}/action");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "server_id", serverId.Value } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken))
                .Select(task => new ConfirmServerResizeApiCall(CreateBasicApiCall(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<RevertServerResizeApiCall> PrepareRevertServerResizeAsync(ServerId serverId, RevertServerResizeRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("servers/{server_id}/action");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "server_id", serverId.Value } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken))
                .Select(task => new RevertServerResizeApiCall(CreateBasicApiCall(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<CreateImageApiCall> PrepareCreateImageAsync(ServerId serverId, CreateImageRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("servers/{server_id}/action");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "server_id", serverId.Value } };

            Func<HttpResponseMessage, CancellationToken, Task<Uri>> deserializeResult =
                (responseMessage, _) =>
                {
                    if (!HttpApiCall.IsAcceptable(responseMessage))
                        throw new HttpWebException(responseMessage);

                    return CompletedTask.FromResult(responseMessage.Headers.Location);
                };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken))
                .Select(task => new CreateImageApiCall(CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        #endregion

        #region Flavors

        /// <inheritdoc/>
        public virtual Task<ListFlavorsApiCall> PrepareListFlavorsAsync(CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("flavors/detail");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            Func<HttpResponseMessage, CancellationToken, Task<ReadOnlyCollectionPage<Flavor>>> deserializeResult =
                (responseMessage, innerCancellationToken) =>
                {
                    Uri originalUri = responseMessage.RequestMessage.RequestUri;

                    if (!HttpApiCall.IsAcceptable(responseMessage))
                        throw new HttpWebException(responseMessage);

                    return responseMessage.Content.ReadAsStringAsync()
                        .Select(
                            innerTask =>
                            {
                                if (string.IsNullOrEmpty(innerTask.Result))
                                    return null;

                                JObject responseObject = JsonConvert.DeserializeObject<JObject>(innerTask.Result);
                                JArray flavorsArray = responseObject["flavors"] as JArray;
                                if (flavorsArray == null)
                                    return null;

                                IList<Flavor> list = flavorsArray.ToObject<Flavor[]>();
                                Func<CancellationToken, Task<ReadOnlyCollectionPage<Flavor>>> getNextPageAsync =
                                    CreateGetNextPageAsyncDelegate<ListFlavorsApiCall, Flavor>(PrepareListFlavorsAsync, responseObject, "flavors");

                                ReadOnlyCollectionPage<Flavor> results = new BasicReadOnlyCollectionPage<Flavor>(list, getNextPageAsync);
                                return results;
                            });
                };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new ListFlavorsApiCall(CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        /// <inheritdoc/>
        public virtual Task<GetFlavorApiCall> PrepareGetFlavorAsync(FlavorId flavorId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("flavors/{flavor_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "flavor_id", flavorId.Value } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetFlavorApiCall(CreateJsonApiCall<FlavorResponse>(task.Result)));
        }

        #endregion

        #region Images

        /// <inheritdoc/>
        public virtual Task<ListImagesApiCall> PrepareListImagesAsync(CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("images");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            Func<HttpResponseMessage, CancellationToken, Task<ReadOnlyCollectionPage<Image>>> deserializeResult =
                (responseMessage, innerCancellationToken) =>
                {
                    Uri originalUri = responseMessage.RequestMessage.RequestUri;

                    if (!HttpApiCall.IsAcceptable(responseMessage))
                        throw new HttpWebException(responseMessage);

                    return responseMessage.Content.ReadAsStringAsync()
                        .Select(
                            innerTask =>
                            {
                                if (string.IsNullOrEmpty(innerTask.Result))
                                    return null;

                                JObject responseObject = JsonConvert.DeserializeObject<JObject>(innerTask.Result);
                                JArray imagesArray = responseObject["images"] as JArray;
                                if (imagesArray == null)
                                    return null;

                                IList<Image> list = imagesArray.ToObject<Image[]>();
                                Func<CancellationToken, Task<ReadOnlyCollectionPage<Image>>> getNextPageAsync =
                                    CreateGetNextPageAsyncDelegate<ListImagesApiCall, Image>(PrepareListImagesAsync, responseObject, "images");

                                ReadOnlyCollectionPage<Image> results = new BasicReadOnlyCollectionPage<Image>(list, getNextPageAsync);
                                return results;
                            });
                };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new ListImagesApiCall(CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        /// <inheritdoc/>
        public virtual Task<GetImageApiCall> PrepareGetImageAsync(ImageId imageId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("images/{image_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "image_id", imageId.Value } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetImageApiCall(CreateJsonApiCall<ImageResponse>(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<RemoveImageApiCall> PrepareRemoveImageAsync(ImageId imageId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("images/{image_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "image_id", imageId.Value } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => new RemoveImageApiCall(CreateBasicApiCall(task.Result)));
        }

        #endregion

        #region Metadata

        /// <inheritdoc/>
        public virtual Task<GetServerMetadataApiCall> PrepareGetServerMetadataAsync(ServerId serverId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("servers/{server_id}/metadata");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "server_id", serverId.Value } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetServerMetadataApiCall(CreateJsonApiCall<MetadataResponse>(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<SetServerMetadataApiCall> PrepareSetServerMetadataAsync(ServerId serverId, MetadataRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("servers/{server_id}/metadata");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "server_id", serverId.Value } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Put, template, parameters, request, cancellationToken))
                .Select(task => new SetServerMetadataApiCall(CreateJsonApiCall<MetadataResponse>(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<GetServerMetadataItemApiCall> PrepareGetServerMetadataItemAsync(ServerId serverId, string key, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("servers/{server_id}/metadata/{key}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "server_id", serverId.Value }, { "key", key } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetServerMetadataItemApiCall(CreateJsonApiCall<MetadataResponse>(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<SetServerMetadataItemApiCall> PrepareSetServerMetadataItemAsync(ServerId serverId, string key, MetadataRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("servers/{server_id}/metadata/{key}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "server_id", serverId.Value }, { "key", key } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Put, template, parameters, request, cancellationToken))
                .Select(task => new SetServerMetadataItemApiCall(CreateJsonApiCall<MetadataResponse>(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<RemoveServerMetadataItemApiCall> PrepareRemoveServerMetadataItemAsync(ServerId serverId, string key, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("servers/{server_id}/metadata/{key}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "server_id", serverId.Value }, { "key", key } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => new RemoveServerMetadataItemApiCall(CreateBasicApiCall(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<GetImageMetadataApiCall> PrepareGetImageMetadataAsync(ImageId imageId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("images/{image_id}/metadata");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "image_id", imageId.Value } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetImageMetadataApiCall(CreateJsonApiCall<MetadataResponse>(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<SetImageMetadataApiCall> PrepareSetImageMetadataAsync(ImageId imageId, MetadataRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("images/{image_id}/metadata");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "image_id", imageId.Value } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Put, template, parameters, request, cancellationToken))
                .Select(task => new SetImageMetadataApiCall(CreateJsonApiCall<MetadataResponse>(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<GetImageMetadataItemApiCall> PrepareGetImageMetadataItemAsync(ImageId imageId, string key, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("images/{image_id}/metadata/{key}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "image_id", imageId.Value }, { "key", key } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetImageMetadataItemApiCall(CreateJsonApiCall<MetadataResponse>(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<SetImageMetadataItemApiCall> PrepareSetImageMetadataItemAsync(ImageId imageId, string key, MetadataRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("images/{image_id}/metadata/{key}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "image_id", imageId.Value }, { "key", key } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Put, template, parameters, request, cancellationToken))
                .Select(task => new SetImageMetadataItemApiCall(CreateJsonApiCall<MetadataResponse>(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<RemoveImageMetadataItemApiCall> PrepareRemoveImageMetadataItemAsync(ImageId imageId, string key, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("images/{image_id}/metadata/{key}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "image_id", imageId.Value }, { "key", key } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => new RemoveImageMetadataItemApiCall(CreateBasicApiCall(task.Result)));
        }

        #endregion

        #region Extensions

        /// <inheritdoc/>
        public virtual Task<ListExtensionsApiCall> PrepareListExtensionsAsync(CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("extensions");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            Func<HttpResponseMessage, CancellationToken, Task<ReadOnlyCollectionPage<Extension>>> deserializeResult =
                (responseMessage, _) =>
                {
                    if (!HttpApiCall.IsAcceptable(responseMessage))
                        throw new HttpWebException(responseMessage);

                    return responseMessage.Content.ReadAsStringAsync()
                        .Select(body => JsonConvert.DeserializeObject<JObject>(body.Result))
                        .Select(
                            obj =>
                            {
                                if (obj.Result == null)
                                    return null;

                                JToken extensions = obj.Result["extensions"];
                                if (extensions == null)
                                    return null;

                                // the pagination for this call, if any, is not documented anywhere
                                ReadOnlyCollectionPage<Extension> result = new BasicReadOnlyCollectionPage<Extension>(extensions.ToObject<Extension[]>(), null);
                                return result;
                            });
                };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new ListExtensionsApiCall(CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        /// <inheritdoc/>
        public virtual Task<GetExtensionApiCall> PrepareGetExtensionAsync(ExtensionAlias alias, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("extensions/{alias}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "alias", alias.Value } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetExtensionApiCall(CreateJsonApiCall<ExtensionResponse>(task.Result)));
        }

        #endregion

        /// <inheritdoc/>
        /// <remarks>
        /// This method calls <see cref="IAuthenticationService.GetBaseAddressAsync"/> to obtain a URI
        /// for the type <c>compute</c>. The preferred name is not specified.
        /// </remarks>
        protected override Task<Uri> GetBaseUriAsyncImpl(CancellationToken cancellationToken)
        {
            const string serviceType = "compute";
            const string serviceName = null;
            return AuthenticationService.GetBaseAddressAsync(serviceType, serviceName, DefaultRegion, _internalUrl, cancellationToken);
        }

        private static Func<CancellationToken, Task<ReadOnlyCollectionPage<TElement>>> CreateGetNextPageAsyncDelegate<TCall, TElement>(Func<CancellationToken, Task<TCall>> prepareApiCall, JObject responseObject, string propertyName)
            where TCall : IHttpApiCall<ReadOnlyCollectionPage<TElement>>
        {
            if (responseObject == null)
                return null;

            JArray linksArray = responseObject[propertyName + "_links"] as JArray;
            if (linksArray == null)
                return null;

            Link[] links = linksArray.ToObject<Link[]>();
            Link nextLink = links.FirstOrDefault(i => string.Equals("next", i.Relation, StringComparison.OrdinalIgnoreCase));
            if (nextLink == null)
                return null;

            return
                cancellationToken =>
                {
                    return
                        CoreTaskExtensions.Using(
                            () => prepareApiCall(cancellationToken)
                                .Select(
                                    _ =>
                                    {
                                        _.Result.RequestMessage.RequestUri = nextLink.Target;
                                        return _.Result;
                                    }),
                            _ => _.Result.SendAsync(cancellationToken))
                        .Select(_ => _.Result.Item2);
                };
        }
    }
}
