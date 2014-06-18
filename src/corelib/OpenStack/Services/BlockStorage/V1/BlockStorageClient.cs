namespace OpenStack.Services.BlockStorage.V1
{
    using System;
    using System.Collections.Generic;
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
    /// This class provides a default implementation of <see cref="IBlockStorageService"/> suitable for
    /// connecting to OpenStack-compatible installations of the Block Storage Service V1.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class BlockStorageClient : ServiceClient, IBlockStorageService
    {
        /// <summary>
        /// Specifies whether the public or internal base address
        /// should be used for accessing the block storage service.
        /// </summary>
        private readonly bool _internalUrl;

        /// <summary>
        /// This field caches the base URI used for accessing the block storage service.
        /// </summary>
        /// <seealso cref="GetBaseUriAsync"/>
        private Uri _baseUri;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlockStorageClient"/> class with the
        /// specified authentication service, default region, and value specifying whether
        /// the internal or public URI should be used for communicating with the block storage service.
        /// </summary>
        /// <param name="authenticationService">The authentication service to use for authenticating requests made to this service.</param>
        /// <param name="defaultRegion">The preferred region for the service. Unless otherwise specified for a specific client, derived service clients will not use a default region if this value is <see langword="null"/> (i.e. only regionless or global service endpoints will be considered acceptable).</param>
        /// <param name="internalUrl"><see langword="true"/> to access the service over a local network; otherwise, <see langword="false"/> to access the service over a public network (the internet).</param>
        public BlockStorageClient(IAuthenticationService authenticationService, string defaultRegion, bool internalUrl)
            : base(authenticationService, defaultRegion)
        {
            _internalUrl = internalUrl;
        }

        #region Volumes

        /// <inheritdoc/>
        public Task<CreateVolumeApiCall> PrepareCreateVolumeAsync(VolumeRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("volumes");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken))
                .Select(task => new CreateVolumeApiCall(CreateJsonApiCall<VolumeResponse>(task.Result)));
        }

        /// <inheritdoc/>
        public Task<ListVolumesApiCall> PrepareListVolumesAsync(CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("volumes");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            Func<HttpResponseMessage, CancellationToken, Task<ReadOnlyCollectionPage<Volume>>> deserializeResult =
                (responseMessage, innerCancellationToken) =>
                {
                    if (!HttpApiCall.IsAcceptable(responseMessage))
                        throw new HttpWebException(responseMessage);

                    return responseMessage.Content.ReadAsStringAsync()
                        .Select(
                            innerTask =>
                            {
                                if (string.IsNullOrEmpty(innerTask.Result))
                                    return null;

                                JObject responseObject = JsonConvert.DeserializeObject<JObject>(innerTask.Result);
                                JArray volumesArray = responseObject["volumes"] as JArray;
                                if (volumesArray == null)
                                    return null;

                                IList<Volume> list = volumesArray.ToObject<Volume[]>();
                                Func<CancellationToken, Task<ReadOnlyCollectionPage<Volume>>> getNextPageAsync = null;

                                ReadOnlyCollectionPage<Volume> results = new BasicReadOnlyCollectionPage<Volume>(list, getNextPageAsync);
                                return results;
                            });
                };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new ListVolumesApiCall(CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        /// <inheritdoc/>
        public Task<GetVolumeApiCall> PrepareGetVolumeAsync(VolumeId volumeId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("volumes/{volume_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "volume_id", volumeId.Value } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetVolumeApiCall(CreateJsonApiCall<VolumeResponse>(task.Result)));
        }

        /// <inheritdoc/>
        public Task<RemoveVolumeApiCall> PrepareRemoveVolumeAsync(VolumeId volumeId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("volumes/{volume_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "volume_id", volumeId.Value } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => new RemoveVolumeApiCall(CreateBasicApiCall(task.Result)));
        }

        #endregion

        #region Snapshots

        /// <inheritdoc/>
        public Task<CreateSnapshotApiCall> PrepareCreateSnapshotAsync(SnapshotRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("snapshots");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken))
                .Select(task => new CreateSnapshotApiCall(CreateJsonApiCall<SnapshotResponse>(task.Result)));
        }

        /// <inheritdoc/>
        public Task<ListSnapshotsApiCall> PrepareListSnapshotsAsync(CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("snapshots");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            Func<HttpResponseMessage, CancellationToken, Task<ReadOnlyCollectionPage<Snapshot>>> deserializeResult =
                (responseMessage, innerCancellationToken) =>
                {
                    if (!HttpApiCall.IsAcceptable(responseMessage))
                        throw new HttpWebException(responseMessage);

                    return responseMessage.Content.ReadAsStringAsync()
                        .Select(
                            innerTask =>
                            {
                                if (string.IsNullOrEmpty(innerTask.Result))
                                    return null;

                                JObject responseObject = JsonConvert.DeserializeObject<JObject>(innerTask.Result);
                                JArray snapshotsArray = responseObject["snapshots"] as JArray;
                                if (snapshotsArray == null)
                                    return null;

                                IList<Snapshot> list = snapshotsArray.ToObject<Snapshot[]>();
                                Func<CancellationToken, Task<ReadOnlyCollectionPage<Snapshot>>> getNextPageAsync = null;

                                ReadOnlyCollectionPage<Snapshot> results = new BasicReadOnlyCollectionPage<Snapshot>(list, getNextPageAsync);
                                return results;
                            });
                };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new ListSnapshotsApiCall(CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        /// <inheritdoc/>
        public Task<GetSnapshotApiCall> PrepareGetSnapshotAsync(SnapshotId snapshotId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("snapshots/{snapshot_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "snapshot_id", snapshotId.Value } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetSnapshotApiCall(CreateJsonApiCall<SnapshotResponse>(task.Result)));
        }

        /// <inheritdoc/>
        public Task<RemoveSnapshotApiCall> PrepareRemoveSnapshotAsync(SnapshotId snapshotId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("snapshots/{snapshot_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "snapshot_id", snapshotId.Value } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => new RemoveSnapshotApiCall(CreateBasicApiCall(task.Result)));
        }

        #endregion

        #region Volume Types

        /// <inheritdoc/>
        public Task<ListVolumeTypesApiCall> PrepareListVolumeTypesAsync(CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("types");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            Func<HttpResponseMessage, CancellationToken, Task<ReadOnlyCollectionPage<VolumeType>>> deserializeResult =
                (responseMessage, innerCancellationToken) =>
                {
                    if (!HttpApiCall.IsAcceptable(responseMessage))
                        throw new HttpWebException(responseMessage);

                    return responseMessage.Content.ReadAsStringAsync()
                        .Select(
                            innerTask =>
                            {
                                if (string.IsNullOrEmpty(innerTask.Result))
                                    return null;

                                JObject responseObject = JsonConvert.DeserializeObject<JObject>(innerTask.Result);
                                JArray volumeTypesArray = responseObject["volume_types"] as JArray;
                                if (volumeTypesArray == null)
                                    return null;

                                IList<VolumeType> list = volumeTypesArray.ToObject<VolumeType[]>();
                                Func<CancellationToken, Task<ReadOnlyCollectionPage<VolumeType>>> getNextPageAsync = null;

                                ReadOnlyCollectionPage<VolumeType> results = new BasicReadOnlyCollectionPage<VolumeType>(list, getNextPageAsync);
                                return results;
                            });
                };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new ListVolumeTypesApiCall(CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        /// <inheritdoc/>
        public Task<GetVolumeTypeApiCall> PrepareGetVolumeTypeAsync(VolumeTypeId volumeTypeId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("types/{volume_type_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "volume_type_id", volumeTypeId.Value } };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetVolumeTypeApiCall(CreateJsonApiCall<VolumeTypeResponse>(task.Result)));
        }

        #endregion

        /// <inheritdoc/>
        /// <remarks>
        /// This method returns a cached base address if one is available. If no cached address is
        /// available, <see cref="IAuthenticationService.GetBaseAddressAsync"/> is called to obtain
        /// a <see cref="Uri"/> with the type <c>volume</c>. The preferred name is not
        /// specified.
        /// </remarks>
        public override Task<Uri> GetBaseUriAsync(CancellationToken cancellationToken)
        {
            if (_baseUri != null)
            {
                return CompletedTask.FromResult(_baseUri);
            }

            const string serviceType = "volume";
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
