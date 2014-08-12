namespace OpenStack.Services.Databases.V1
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
    using Link = OpenStack.Services.Compute.V2.Link;

    /// <summary>
    /// This class provides a default implementation of <see cref="IDatabaseService"/> suitable for
    /// connecting to OpenStack-compatible installations of the Database Service V1.
    /// </summary>
    /// <seealso href="http://developer.openstack.org/api-ref-databases-v1.html">OpenStack Database Service API v1.0</seealso>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class DatabaseClient : ServiceClient, IDatabaseService
    {
        /// <summary>
        /// Specifies whether the public or internal base address
        /// should be used for accessing the Database Service.
        /// </summary>
        private readonly bool _internalUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseClient"/> class with the
        /// specified authentication service, default region, and value indicating whether
        /// an internal or public endpoint should be used for communicating with the
        /// service.
        /// </summary>
        /// <param name="authenticationService">The authentication service to use for authenticating requests made to this service.</param>
        /// <param name="defaultRegion">The preferred region for the service. Unless otherwise specified for a specific client, derived service clients will not use a default region if this value is <see langword="null"/> (i.e. only regionless or global service endpoints will be considered acceptable).</param>
        /// <param name="internalUrl"><see langword="true"/> to access the service over a local network; otherwise, <see langword="false"/> to access the service over a public network (the internet).</param>
        /// <exception cref="ArgumentNullException">If <paramref name="authenticationService"/> is <see langword="null"/>.</exception>
        public DatabaseClient(IAuthenticationService authenticationService, string defaultRegion, bool internalUrl)
            : base(authenticationService, defaultRegion)
        {
            _internalUrl = internalUrl;
        }

        /// <summary>
        /// Gets a value indicating whether the service should be accessed over a local or public network.
        /// </summary>
        /// <value>
        /// <see langword="true"/> to access the service over a local network.
        /// <para>-or-</para>
        /// <para><see langword="false"/> to access the service over a public network (the internet).</para>
        /// </value>
        protected bool InternalUrl
        {
            get
            {
                return _internalUrl;
            }
        }

        #region IDatabaseService Members

        #region Database instances

        /// <inheritdoc/>
        public Task<CreateDatabaseInstanceApiCall> PrepareCreateDatabaseInstanceAsync(DatabaseInstanceRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            UriTemplate template = new UriTemplate("instances");
            var parameters = new Dictionary<string, string>();
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken))
                .Select(task => new CreateDatabaseInstanceApiCall(CreateJsonApiCall<DatabaseInstanceResponse>(task.Result)));
        }

        /// <inheritdoc/>
        public Task<ListDatabaseInstancesApiCall> PrepareListDatabaseInstancesAsync(CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("instances");
            var parameters = new Dictionary<string, string>();

            Func<HttpResponseMessage, CancellationToken, Task<ReadOnlyCollectionPage<DatabaseInstance>>> deserializeResult =
                (responseMessage, _) =>
                {
                    if (!HttpApiCall.IsAcceptable(responseMessage))
                        throw new HttpWebException(responseMessage);

                    Uri originalUri = responseMessage.RequestMessage.RequestUri;
                    return responseMessage.Content.ReadAsStringAsync()
                        .Select(
                            innerTask =>
                            {
                                if (string.IsNullOrEmpty(innerTask.Result))
                                    return null;

                                JObject responseObject = JsonConvert.DeserializeObject<JObject>(innerTask.Result);
                                JArray databaseInstancesArray = responseObject["instances"] as JArray;
                                if (databaseInstancesArray == null)
                                    return null;

                                IList<DatabaseInstance> list = databaseInstancesArray.ToObject<DatabaseInstance[]>();

                                // According to the Rackspace documentation, this call is paginated. Pagination is not mentioned
                                // at all in the OpenStack reference.
                                // http://docs.rackspace.com/cdb/api/v1.0/cdb-devguide/content/pagination.html
                                Func<CancellationToken, Task<ListDatabaseInstancesApiCall>> prepareApiCall = PrepareListDatabaseInstancesAsync;
                                Func<CancellationToken, Task<ReadOnlyCollectionPage<DatabaseInstance>>> getNextPageAsync =
                                    CreateGetNextPageAsyncDelegate<ListDatabaseInstancesApiCall, DatabaseInstance>(prepareApiCall, responseObject);

                                ReadOnlyCollectionPage<DatabaseInstance> results = new BasicReadOnlyCollectionPage<DatabaseInstance>(list, getNextPageAsync);
                                return results;
                            });
                };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new ListDatabaseInstancesApiCall(CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        /// <inheritdoc/>
        public Task<GetDatabaseInstanceApiCall> PrepareGetDatabaseInstanceAsync(DatabaseInstanceId instanceId, CancellationToken cancellationToken)
        {
            if (instanceId == null)
                throw new ArgumentNullException("instanceId");

            UriTemplate template = new UriTemplate("instances/{instance_id}");
            var parameters = new Dictionary<string, string> { { "instance_id", instanceId.Value } };
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetDatabaseInstanceApiCall(CreateJsonApiCall<DatabaseInstanceResponse>(task.Result)));
        }

        /// <inheritdoc/>
        public Task<RemoveDatabaseInstanceApiCall> PrepareRemoveDatabaseInstanceAsync(DatabaseInstanceId instanceId, CancellationToken cancellationToken)
        {
            if (instanceId == null)
                throw new ArgumentNullException("instanceId");

            UriTemplate template = new UriTemplate("instances/{instance_id}");
            var parameters = new Dictionary<string, string> { { "instance_id", instanceId.Value } };
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => new RemoveDatabaseInstanceApiCall(CreateBasicApiCall(task.Result)));
        }

        /// <inheritdoc/>
        public Task<EnableRootUserApiCall> PrepareEnableRootUserAsync(DatabaseInstanceId instanceId, CancellationToken cancellationToken)
        {
            if (instanceId == null)
                throw new ArgumentNullException("instanceId");

            UriTemplate template = new UriTemplate("instances/{instance_id}/root");
            var parameters = new Dictionary<string, string> { { "instance_id", instanceId.Value } };
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, cancellationToken))
                .Select(task => new EnableRootUserApiCall(CreateJsonApiCall<DatabaseUserResponse>(task.Result)));
        }

        /// <inheritdoc/>
        public Task<CheckRootEnabledApiCall> PrepareCheckRootEnabledAsync(DatabaseInstanceId instanceId, CancellationToken cancellationToken)
        {
            if (instanceId == null)
                throw new ArgumentNullException("instanceId");

            UriTemplate template = new UriTemplate("instances/{instance_id}/root");
            var parameters = new Dictionary<string, string> { { "instance_id", instanceId.Value } };
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new CheckRootEnabledApiCall(CreateJsonApiCall<RootEnabledResponse>(task.Result)));
        }

        #endregion

        #region Database instance actions

        /// <inheritdoc/>
        public Task<RestartDatabaseInstanceApiCall> PrepareRestartDatabaseInstanceAsync(DatabaseInstanceId instanceId, RestartRequest request, CancellationToken cancellationToken)
        {
            if (instanceId == null)
                throw new ArgumentNullException("instanceId");
            if (request == null)
                throw new ArgumentNullException("request");

            UriTemplate template = new UriTemplate("instances/{instance_id}/action");
            var parameters = new Dictionary<string, string> { { "instance_id", instanceId.Value } };
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken))
                .Select(task => new RestartDatabaseInstanceApiCall(CreateBasicApiCall(task.Result)));
        }

        /// <inheritdoc/>
        public Task<ResizeDatabaseInstanceApiCall> PrepareResizeDatabaseInstanceAsync(DatabaseInstanceId instanceId, ResizeRequest request, CancellationToken cancellationToken)
        {
            if (instanceId == null)
                throw new ArgumentNullException("instanceId");
            if (request == null)
                throw new ArgumentNullException("request");

            UriTemplate template = new UriTemplate("instances/{instance_id}/action");
            var parameters = new Dictionary<string, string> { { "instance_id", instanceId.Value } };
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken))
                .Select(task => new ResizeDatabaseInstanceApiCall(CreateBasicApiCall(task.Result)));
        }

        /// <inheritdoc/>
        public Task<ResizeDatabaseInstanceVolumeApiCall> PrepareResizeDatabaseInstanceVolumeAsync(DatabaseInstanceId instanceId, ResizeVolumeRequest request, CancellationToken cancellationToken)
        {
            if (instanceId == null)
                throw new ArgumentNullException("instanceId");
            if (request == null)
                throw new ArgumentNullException("request");

            UriTemplate template = new UriTemplate("instances/{instance_id}/action");
            var parameters = new Dictionary<string, string> { { "instance_id", instanceId.Value } };
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken))
                .Select(task => new ResizeDatabaseInstanceVolumeApiCall(CreateBasicApiCall(task.Result)));
        }

        #endregion

        #region Databases

        /// <inheritdoc/>
        public Task<CreateDatabasesApiCall> PrepareCreateDatabasesAsync(DatabaseInstanceId instanceId, DatabasesRequest request, CancellationToken cancellationToken)
        {
            if (instanceId == null)
                throw new ArgumentNullException("instanceId");
            if (request == null)
                throw new ArgumentNullException("request");

            UriTemplate template = new UriTemplate("instances/{instance_id}/databases");
            var parameters = new Dictionary<string, string> { { "instance_id", instanceId.Value } };
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken))
                .Select(task => new CreateDatabasesApiCall(CreateBasicApiCall(task.Result)));
        }

        /// <inheritdoc/>
        public Task<ListDatabasesApiCall> PrepareListDatabasesAsync(DatabaseInstanceId instanceId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("instances/{instance_id}/databases");
            var parameters = new Dictionary<string, string> { { "instance_id", instanceId.Value } };

            Func<HttpResponseMessage, CancellationToken, Task<ReadOnlyCollectionPage<Database>>> deserializeResult =
                (responseMessage, _) =>
                {
                    if (!HttpApiCall.IsAcceptable(responseMessage))
                        throw new HttpWebException(responseMessage);

                    Uri originalUri = responseMessage.RequestMessage.RequestUri;
                    return responseMessage.Content.ReadAsStringAsync()
                        .Select(
                            innerTask =>
                            {
                                if (string.IsNullOrEmpty(innerTask.Result))
                                    return null;

                                JObject responseObject = JsonConvert.DeserializeObject<JObject>(innerTask.Result);
                                JArray databasesArray = responseObject["databases"] as JArray;
                                if (databasesArray == null)
                                    return null;

                                IList<Database> list = databasesArray.ToObject<Database[]>();

                                // According to the Rackspace documentation, this call is paginated. Pagination is not mentioned
                                // at all in the OpenStack reference.
                                // http://docs.rackspace.com/cdb/api/v1.0/cdb-devguide/content/pagination.html
                                Func<CancellationToken, Task<ListDatabasesApiCall>> prepareApiCall =
                                    innerCancellationToken => PrepareListDatabasesAsync(instanceId, innerCancellationToken);
                                Func<CancellationToken, Task<ReadOnlyCollectionPage<Database>>> getNextPageAsync =
                                    CreateGetNextPageAsyncDelegate<ListDatabasesApiCall, Database>(prepareApiCall, responseObject);

                                ReadOnlyCollectionPage<Database> results = new BasicReadOnlyCollectionPage<Database>(list, getNextPageAsync);
                                return results;
                            });
                };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new ListDatabasesApiCall(CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        /// <inheritdoc/>
        public Task<RemoveDatabaseApiCall> PrepareRemoveDatabaseAsync(DatabaseInstanceId instanceId, DatabaseName databaseName, CancellationToken cancellationToken)
        {
            if (instanceId == null)
                throw new ArgumentNullException("instanceId");
            if (databaseName == null)
                throw new ArgumentNullException("databaseName");

            UriTemplate template = new UriTemplate("instances/{instance_id}/databases/{database_name}");
            var parameters = new Dictionary<string, string> { { "instance_id", instanceId.Value }, { "database_name", databaseName.Value } };
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => new RemoveDatabaseApiCall(CreateBasicApiCall(task.Result)));
        }

        #endregion

        #region Users

        /// <inheritdoc/>
        public Task<CreateUsersApiCall> PrepareCreateUsersAsync(DatabaseInstanceId instanceId, UsersRequest request, CancellationToken cancellationToken)
        {
            if (instanceId == null)
                throw new ArgumentNullException("instanceId");
            if (request == null)
                throw new ArgumentNullException("request");

            UriTemplate template = new UriTemplate("instances/{instance_id}/users");
            var parameters = new Dictionary<string, string> { { "instance_id", instanceId.Value } };
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken))
                .Select(task => new CreateUsersApiCall(CreateBasicApiCall(task.Result)));
        }

        /// <inheritdoc/>
        public Task<ListUsersApiCall> PrepareListUsersAsync(DatabaseInstanceId instanceId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("instances/{instance_id}/users");
            var parameters = new Dictionary<string, string> { { "instance_id", instanceId.Value } };

            Func<HttpResponseMessage, CancellationToken, Task<ReadOnlyCollectionPage<DatabaseUser>>> deserializeResult =
                (responseMessage, _) =>
                {
                    if (!HttpApiCall.IsAcceptable(responseMessage))
                        throw new HttpWebException(responseMessage);

                    Uri originalUri = responseMessage.RequestMessage.RequestUri;
                    return responseMessage.Content.ReadAsStringAsync()
                        .Select(
                            innerTask =>
                            {
                                if (string.IsNullOrEmpty(innerTask.Result))
                                    return null;

                                JObject responseObject = JsonConvert.DeserializeObject<JObject>(innerTask.Result);
                                JArray usersArray = responseObject["users"] as JArray;
                                if (usersArray == null)
                                    return null;

                                IList<DatabaseUser> list = usersArray.ToObject<DatabaseUser[]>();

                                // According to the Rackspace documentation, this call is paginated. Pagination is not mentioned
                                // at all in the OpenStack reference.
                                // http://docs.rackspace.com/cdb/api/v1.0/cdb-devguide/content/pagination.html
                                Func<CancellationToken, Task<ListUsersApiCall>> prepareApiCall =
                                    innerCancellationToken => PrepareListUsersAsync(instanceId, innerCancellationToken);
                                Func<CancellationToken, Task<ReadOnlyCollectionPage<DatabaseUser>>> getNextPageAsync =
                                    CreateGetNextPageAsyncDelegate<ListUsersApiCall, DatabaseUser>(prepareApiCall, responseObject);

                                ReadOnlyCollectionPage<DatabaseUser> results = new BasicReadOnlyCollectionPage<DatabaseUser>(list, getNextPageAsync);
                                return results;
                            });
                };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new ListUsersApiCall(CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        /// <inheritdoc/>
        public Task<RemoveUserApiCall> PrepareRemoveUserAsync(DatabaseInstanceId instanceId, UserName userName, CancellationToken cancellationToken)
        {
            if (instanceId == null)
                throw new ArgumentNullException("instanceId");
            if (userName == null)
                throw new ArgumentNullException("userName");

            UriTemplate template = new UriTemplate("instances/{instance_id}/users/{user_name}");
            var parameters = new Dictionary<string, string> { { "instance_id", instanceId.Value }, { "user_name", userName.Value } };
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => new RemoveUserApiCall(CreateBasicApiCall(task.Result)));
        }

        #endregion

        #region Flavors

        /// <inheritdoc/>
        public Task<ListFlavorsApiCall> PrepareListFlavorsAsync(CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("flavors");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            Func<HttpResponseMessage, CancellationToken, Task<ReadOnlyCollectionPage<DatabaseFlavor>>> deserializeResult =
                (responseMessage, _) =>
                {
                    if (!HttpApiCall.IsAcceptable(responseMessage))
                        throw new HttpWebException(responseMessage);

                    Uri originalUri = responseMessage.RequestMessage.RequestUri;
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

                                IList<DatabaseFlavor> list = flavorsArray.ToObject<DatabaseFlavor[]>();

                                // According to the Rackspace documentation, this call is not paginated. Pagination is not mentioned
                                // at all in the OpenStack reference. For maximum flexibility and consistency with other methods in
                                // this client which return collections, this method allows for pagination provided the vendor
                                // implements pagination in a manner compatible with the Rackspace pagination documentation.
                                // http://docs.rackspace.com/cdb/api/v1.0/cdb-devguide/content/pagination.html
                                Func<CancellationToken, Task<ListFlavorsApiCall>> prepareApiCall = PrepareListFlavorsAsync;
                                Func<CancellationToken, Task<ReadOnlyCollectionPage<DatabaseFlavor>>> getNextPageAsync =
                                    CreateGetNextPageAsyncDelegate<ListFlavorsApiCall, DatabaseFlavor>(prepareApiCall, responseObject);

                                ReadOnlyCollectionPage<DatabaseFlavor> results = new BasicReadOnlyCollectionPage<DatabaseFlavor>(list, getNextPageAsync);
                                return results;
                            });
                };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new ListFlavorsApiCall(CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        /// <inheritdoc/>
        public Task<GetFlavorApiCall> PrepareGetFlavorAsync(FlavorId flavorId, CancellationToken cancellationToken)
        {
            if (flavorId == null)
                throw new ArgumentNullException("flavorId");

            UriTemplate template = new UriTemplate("flavors/{flavor_id}");
            var parameters = new Dictionary<string, string> { { "flavor_id", flavorId.Value } };
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetFlavorApiCall(CreateJsonApiCall<DatabaseFlavorResponse>(task.Result)));
        }

        #endregion

        #endregion

        /// <inheritdoc/>
        /// <remarks>
        /// This method calls <see cref="IAuthenticationService.GetBaseAddressAsync"/> to obtain a URI
        /// for the type <c>databases</c>. The preferred name is not specified.
        /// </remarks>
        protected override Task<Uri> GetBaseUriAsyncImpl(CancellationToken cancellationToken)
        {
            const string serviceType = "databases";
            const string serviceName = "";
            return AuthenticationService.GetBaseAddressAsync(serviceType, serviceName, DefaultRegion, _internalUrl, cancellationToken);
        }

        /// <summary>
        /// This method implements support for the <see cref="ReadOnlyCollectionPage{T}.GetNextPageAsync"/> method
        /// for collections returned by the Database Service according to the documentation provided by Rackspace.
        /// </summary>
        /// <remarks>
        /// <note type="warning">
        /// The OpenStack documentation does not mention pagination at all. This implementation supports providers
        /// that do not use pagination at all, along with providers that implement pagination in a manner compatible
        /// with the Rackspace documentation.
        /// </note>
        /// </remarks>
        /// <typeparam name="TCall">The type representing the prepared HTTP API call which returns a page of the paginated collection.</typeparam>
        /// <typeparam name="TElement">The type element contained in the paginated collection.</typeparam>
        /// <param name="prepareApiCall">A delegate which asynchronously prepares the HTTP API call to obtain a page of the collection.</param>
        /// <param name="responseObject">The JSON object returned for the previous page of results.</param>
        /// <returns>
        /// A delegate which implements the <see cref="ReadOnlyCollectionPage{T}.GetNextPageAsync"/> behavior
        /// for a paginated collection returned by API calls to this service.
        /// </returns>
        protected virtual Func<CancellationToken, Task<ReadOnlyCollectionPage<TElement>>> CreateGetNextPageAsyncDelegate<TCall, TElement>(Func<CancellationToken, Task<TCall>> prepareApiCall, JObject responseObject)
            where TCall : IHttpApiCall<ReadOnlyCollectionPage<TElement>>
        {
            if (responseObject == null)
                return null;

            JArray linksArray = responseObject["links"] as JArray;
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
                                        _.Result.RequestMessage.RequestUri = new Uri(_.Result.RequestMessage.RequestUri, nextLink.Target);
                                        return _.Result;
                                    }),
                            _ => _.Result.SendAsync(cancellationToken))
                        .Select(_ => _.Result.Item2);
                };
        }
    }
}
