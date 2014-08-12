namespace Rackspace.Services.Databases.V1
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
    using OpenStack.Services;
    using OpenStack.Services.Databases.V1;
    using Rackspace.Net;
    using Rackspace.Threading;
    using Link = OpenStack.Services.Compute.V2.Link;

    public static class RackspaceDatabaseServiceExtensions
    {
        #region Users

        /// <summary>
        /// Prepare an API call to set the password for one or more database users.
        /// </summary>
        /// <param name="service">The <see cref="IDatabaseService"/> instance.</param>
        /// <param name="instanceId">The database instance ID.</param>
        /// <param name="request">A <see cref="UsersRequest"/> instance containing a <see cref="DatabaseUserData"/> with the username and new password of each user to reset passwords for.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the prepared API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="instanceId"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="request"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the preparation of this API call.</exception>
        /// <seealso cref="SetUserPasswordsApiCall"/>
        /// <seealso cref="RackspaceDatabaseServiceExtensions.SetUserPasswordsAsync"/>
        /// <seealso href="http://docs.rackspace.com/cdb/api/v1.0/cdb-devguide/content/PUT_changePass__version___accountId__instances__instanceId__users_.html">Change User(s) Password (Rackspace Cloud Databases Developer Guide - API v1.0)</seealso>
        public static Task<SetUserPasswordsApiCall> PrepareSetUserPasswordsAsync(this IDatabaseService service, DatabaseInstanceId instanceId, UsersRequest request, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");
            if (instanceId == null)
                throw new ArgumentNullException("instanceId");
            if (request == null)
                throw new ArgumentNullException("request");

            UriTemplate template = new UriTemplate("instances/{instance_id}/users");
            var parameters = new Dictionary<string, string> { { "instance_id", instanceId.Value } };

            IHttpApiCallFactory factory = service.GetHttpApiCallFactory();
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Put, template, parameters, request, cancellationToken))
                .Select(task => new SetUserPasswordsApiCall(factory.CreateBasicApiCall(task.Result, HttpCompletionOption.ResponseContentRead)));
        }

        /// <summary>
        /// Prepare an API call to update the properties of a user in a database instance.
        /// </summary>
        /// <param name="service">The <see cref="IDatabaseService"/> instance.</param>
        /// <param name="instanceId">The database instance ID.</param>
        /// <param name="userName">The username of the user.</param>
        /// <param name="request">A <see cref="UserRequest"/> containing the updated properties for the user.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the prepared API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="instanceId"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="userName"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="request"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the preparation of this API call.</exception>
        /// <seealso cref="UpdateUserApiCall"/>
        /// <seealso cref="RackspaceDatabaseServiceExtensions.UpdateUserAsync"/>
        /// <seealso href="http://docs.rackspace.com/cdb/api/v1.0/cdb-devguide/content/PUT_modifyUser__version___accountId__instances__instanceId__users__name__.html">Modify User Attributes (Rackspace Cloud Databases Developer Guide - API v1.0)</seealso>
        public static Task<UpdateUserApiCall> PrepareUpdateUserAsync(this IDatabaseService service, DatabaseInstanceId instanceId, UserName userName, UserRequest request, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");
            if (instanceId == null)
                throw new ArgumentNullException("instanceId");
            if (userName == null)
                throw new ArgumentNullException("userName");
            if (request == null)
                throw new ArgumentNullException("request");

            UriTemplate template = new UriTemplate("instances/{instance_id}/users/{user_name}");
            var parameters = new Dictionary<string, string> { { "instance_id", instanceId.Value }, { "user_name", userName.Value } };

            IHttpApiCallFactory factory = service.GetHttpApiCallFactory();
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Put, template, parameters, request, cancellationToken))
                .Select(task => new UpdateUserApiCall(factory.CreateBasicApiCall(task.Result, HttpCompletionOption.ResponseContentRead)));
        }

        /// <summary>
        /// Prepare an API call to get a user resource within a database instance.
        /// </summary>
        /// <param name="service">The <see cref="IDatabaseService"/> instance.</param>
        /// <param name="instanceId">The database instance ID.</param>
        /// <param name="userName">The username of the user.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the prepared API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="instanceId"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="userName"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the preparation of this API call.</exception>
        /// <seealso cref="GetUserApiCall"/>
        /// <seealso cref="RackspaceDatabaseServiceExtensions.GetUserAsync"/>
        /// <seealso href="http://docs.rackspace.com/cdb/api/v1.0/cdb-devguide/content/GET_listUser__version___accountId__instances__instanceId__users__name__.html">List User (Rackspace Cloud Databases Developer Guide - API v1.0)</seealso>
        public static Task<GetUserApiCall> PrepareGetUserAsync(this IDatabaseService service, DatabaseInstanceId instanceId, UserName userName, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");
            if (instanceId == null)
                throw new ArgumentNullException("instanceId");
            if (userName == null)
                throw new ArgumentNullException("userName");

            UriTemplate template = new UriTemplate("instances/{instance_id}/users/{user_name}");
            var parameters = new Dictionary<string, string> { { "instance_id", instanceId.Value }, { "user_name", userName.Value } };

            IHttpApiCallFactory factory = service.GetHttpApiCallFactory();
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetUserApiCall(factory.CreateJsonApiCall<UserResponse>(task.Result)));
        }

        /// <summary>
        /// Prepare an API call to get a list of databases a user has permission to access.
        /// </summary>
        /// <param name="service">The <see cref="IDatabaseService"/> instance.</param>
        /// <param name="instanceId">The database instance ID.</param>
        /// <param name="userName">The username of the user.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the prepared API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="instanceId"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="userName"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the preparation of this API call.</exception>
        /// <seealso cref="ListUserAccessApiCall"/>
        /// <seealso cref="RackspaceDatabaseServiceExtensions.ListUserAccessAsync"/>
        /// <seealso href="http://docs.rackspace.com/cdb/api/v1.0/cdb-devguide/content/GET_getUserAccess__version___accountId__instances__instanceId__users__name__databases_.html">List User Access (Rackspace Cloud Databases Developer Guide - API v1.0)</seealso>
        public static Task<ListUserAccessApiCall> PrepareListUserAccessAsync(this IDatabaseService service, DatabaseInstanceId instanceId, UserName userName, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");
            if (instanceId == null)
                throw new ArgumentNullException("instanceId");
            if (userName == null)
                throw new ArgumentNullException("userName");

            UriTemplate template = new UriTemplate("instances/{instance_id}/users/{user_name}/databases");
            var parameters = new Dictionary<string, string> { { "instance_id", instanceId.Value }, { "user_name", userName.Value } };

            Func<HttpResponseMessage, CancellationToken, Task<ReadOnlyCollectionPage<DatabaseName>>> deserializeResult =
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

                                Database[] databases = databasesArray.ToObject<Database[]>();
                                IList<DatabaseName> list = databases.ConvertAll(database => database.Name);

                                // According to the Rackspace documentation, this call is not paginated. Pagination is not mentioned
                                // at all in the OpenStack reference. For maximum flexibility and consistency with other methods in
                                // this client which return collections, this method allows for pagination provided the vendor
                                // implements pagination in a manner compatible with the Rackspace pagination documentation.
                                // http://docs.rackspace.com/cdb/api/v1.0/cdb-devguide/content/pagination.html
                                Func<CancellationToken, Task<ListUserAccessApiCall>> prepareApiCall =
                                    innerCancellationToken => service.PrepareListUserAccessAsync(instanceId, userName, innerCancellationToken);
                                Func<CancellationToken, Task<ReadOnlyCollectionPage<DatabaseName>>> getNextPageAsync =
                                    CreateGetNextPageAsyncDelegate<ListUserAccessApiCall, DatabaseName>(prepareApiCall, responseObject);

                                ReadOnlyCollectionPage<DatabaseName> results = new BasicReadOnlyCollectionPage<DatabaseName>(list, getNextPageAsync);
                                return results;
                            });
                };

            IHttpApiCallFactory factory = service.GetHttpApiCallFactory();
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new ListUserAccessApiCall(factory.CreateCustomApiCall<ReadOnlyCollectionPage<DatabaseName>>(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        /// <summary>
        /// Prepare an API call to grant access to one or more databases for a particular user.
        /// </summary>
        /// <param name="service">The <see cref="IDatabaseService"/> instance.</param>
        /// <param name="instanceId">The database instance ID.</param>
        /// <param name="userName">The username of the user.</param>
        /// <param name="request">A <see cref="DatabasesRequest"/> instance containing the names of databases which the user should be granted access to.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the prepared API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="instanceId"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="userName"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="request"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the preparation of this API call.</exception>
        /// <seealso cref="GrantUserAccessApiCall"/>
        /// <seealso cref="RackspaceDatabaseServiceExtensions.GrantUserAccessAsync"/>
        /// <seealso href="http://docs.rackspace.com/cdb/api/v1.0/cdb-devguide/content/PUT_grantUserAccess__version___accountId__instances__instanceId__users__name__databases_.html">Grant User Access (Rackspace Cloud Databases Developer Guide - API v1.0)</seealso>
        public static Task<GrantUserAccessApiCall> PrepareGrantUserAccessAsync(this IDatabaseService service, DatabaseInstanceId instanceId, UserName userName, DatabasesRequest request, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");
            if (instanceId == null)
                throw new ArgumentNullException("instanceId");
            if (userName == null)
                throw new ArgumentNullException("userName");
            if (request == null)
                throw new ArgumentNullException("request");

            UriTemplate template = new UriTemplate("instances/{instance_id}/users/{user_name}/databases");
            var parameters = new Dictionary<string, string> { { "instance_id", instanceId.Value }, { "user_name", userName.Value } };

            IHttpApiCallFactory factory = service.GetHttpApiCallFactory();
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Put, template, parameters, request, cancellationToken))
                .Select(task => new GrantUserAccessApiCall(factory.CreateBasicApiCall(task.Result, HttpCompletionOption.ResponseContentRead)));
        }

        /// <summary>
        /// Prepare an API call to revoke access to a database for a particular user.
        /// </summary>
        /// <param name="service">The <see cref="IDatabaseService"/> instance.</param>
        /// <param name="instanceId">The database instance ID.</param>
        /// <param name="userName">The username of the user.</param>
        /// <param name="databaseName">The name of the database to revoke access to.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the prepared API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="instanceId"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="userName"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="databaseName"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the preparation of this API call.</exception>
        /// <seealso cref="RevokeUserAccessApiCall"/>
        /// <seealso cref="RackspaceDatabaseServiceExtensions.RevokeUserAccessAsync"/>
        /// <seealso href="http://docs.rackspace.com/cdb/api/v1.0/cdb-devguide/content/DELETE_revokeUserAccess__version___accountId__instances__instanceId__users__name__databases__databaseName__.html">Revoke User Access (Rackspace Cloud Databases Developer Guide - API v1.0)</seealso>
        public static Task<RevokeUserAccessApiCall> PrepareRevokeUserAccessAsync(this IDatabaseService service, DatabaseInstanceId instanceId, UserName userName, DatabaseName databaseName, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");
            if (instanceId == null)
                throw new ArgumentNullException("instanceId");
            if (userName == null)
                throw new ArgumentNullException("userName");
            if (databaseName == null)
                throw new ArgumentNullException("databaseName");

            UriTemplate template = new UriTemplate("instances/{instance_id}/users/{user_name}/databases/{database_name}");
            var parameters = new Dictionary<string, string> { { "instance_id", instanceId.Value }, { "user_name", userName.Value }, { "database_name", databaseName.Value } };

            IHttpApiCallFactory factory = service.GetHttpApiCallFactory();
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => new RevokeUserAccessApiCall(factory.CreateBasicApiCall(task.Result, HttpCompletionOption.ResponseContentRead)));
        }

        #endregion

        /// <summary>
        /// Set the password for one or more database users.
        /// </summary>
        /// <param name="service">The <see cref="IDatabaseService"/> instance.</param>
        /// <param name="instanceId">The database instance ID.</param>
        /// <param name="users">A collection of <see cref="DatabaseUserData"/> instances containing the username and new password of each user to reset passwords for.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the prepared API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="instanceId"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="users"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">If <paramref name="users"/> contains any <see langword="null"/> values.</exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <seealso cref="RackspaceDatabaseServiceExtensions.PrepareSetUserPasswordsAsync"/>
        /// <seealso href="http://docs.rackspace.com/cdb/api/v1.0/cdb-devguide/content/PUT_changePass__version___accountId__instances__instanceId__users_.html">Change User(s) Password (Rackspace Cloud Databases Developer Guide - API v1.0)</seealso>
        public static Task SetUserPasswordsAsync(this IDatabaseService service, DatabaseInstanceId instanceId, IEnumerable<DatabaseUserData> users, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareSetUserPasswordsAsync(instanceId, new UsersRequest(users), cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        /// <summary>
        /// Update the properties of a user in a database instance.
        /// </summary>
        /// <param name="service">The <see cref="IDatabaseService"/> instance.</param>
        /// <param name="instanceId">The database instance ID.</param>
        /// <param name="userName">The username of the user.</param>
        /// <param name="user">A <see cref="DatabaseUserData"/> instance containing the updated properties for the user.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the prepared API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="instanceId"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="userName"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="user"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <seealso cref="RackspaceDatabaseServiceExtensions.PrepareUpdateUserAsync"/>
        /// <seealso href="http://docs.rackspace.com/cdb/api/v1.0/cdb-devguide/content/PUT_modifyUser__version___accountId__instances__instanceId__users__name__.html">Modify User Attributes (Rackspace Cloud Databases Developer Guide - API v1.0)</seealso>
        public static Task UpdateUserAsync(this IDatabaseService service, DatabaseInstanceId instanceId, UserName userName, DatabaseUserData user, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareUpdateUserAsync(instanceId, userName, new UserRequest(user), cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        /// <summary>
        /// Get a user resource within a database instance.
        /// </summary>
        /// <param name="service">The <see cref="IDatabaseService"/> instance.</param>
        /// <param name="instanceId">The database instance ID.</param>
        /// <param name="userName">The username of the user.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the prepared API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="instanceId"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="userName"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <seealso cref="RackspaceDatabaseServiceExtensions.PrepareGetUserAsync"/>
        /// <seealso href="http://docs.rackspace.com/cdb/api/v1.0/cdb-devguide/content/GET_listUser__version___accountId__instances__instanceId__users__name__.html">List User (Rackspace Cloud Databases Developer Guide - API v1.0)</seealso>
        public static Task<DatabaseUser> GetUserAsync(this IDatabaseService service, DatabaseInstanceId instanceId, UserName userName, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetUserAsync(instanceId, userName, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.User);
        }

        /// <summary>
        /// Get a list of databases a user has permission to access.
        /// </summary>
        /// <param name="service">The <see cref="IDatabaseService"/> instance.</param>
        /// <param name="instanceId">The database instance ID.</param>
        /// <param name="userName">The username of the user.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the prepared API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="instanceId"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="userName"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <seealso cref="RackspaceDatabaseServiceExtensions.PrepareListUserAccessAsync"/>
        /// <seealso href="http://docs.rackspace.com/cdb/api/v1.0/cdb-devguide/content/GET_getUserAccess__version___accountId__instances__instanceId__users__name__databases_.html">List User Access (Rackspace Cloud Databases Developer Guide - API v1.0)</seealso>
        public static Task<ReadOnlyCollectionPage<DatabaseName>> ListUserAccessAsync(this IDatabaseService service, DatabaseInstanceId instanceId, UserName userName, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareListUserAccessAsync(instanceId, userName, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        /// <summary>
        /// Grant access to one or more databases for a particular user.
        /// </summary>
        /// <param name="service">The <see cref="IDatabaseService"/> instance.</param>
        /// <param name="instanceId">The database instance ID.</param>
        /// <param name="userName">The username of the user.</param>
        /// <param name="databases">A collection of <see cref="DatabaseName"/> instances identifying the databases to grant access to.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the prepared API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="instanceId"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="userName"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="databases"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">If <paramref name="databases"/> contains any <see langword="null"/> values.</exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <seealso cref="RackspaceDatabaseServiceExtensions.PrepareGrantUserAccessAsync"/>
        /// <seealso href="http://docs.rackspace.com/cdb/api/v1.0/cdb-devguide/content/PUT_grantUserAccess__version___accountId__instances__instanceId__users__name__databases_.html">Grant User Access (Rackspace Cloud Databases Developer Guide - API v1.0)</seealso>
        public static Task GrantUserAccessAsync(this IDatabaseService service, DatabaseInstanceId instanceId, UserName userName, IEnumerable<DatabaseName> databases, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareGrantUserAccessAsync(instanceId, userName, new DatabasesRequest(databases), cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        /// <summary>
        /// Revoke access to a database for a particular user.
        /// </summary>
        /// <param name="service">The <see cref="IDatabaseService"/> instance.</param>
        /// <param name="instanceId">The database instance ID.</param>
        /// <param name="userName">The username of the user.</param>
        /// <param name="databaseName">The name of the database to revoke access to.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the prepared API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="instanceId"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="userName"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="databaseName"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <seealso cref="RevokeUserAccessApiCall"/>
        /// <seealso cref="RackspaceDatabaseServiceExtensions.RevokeUserAccessAsync"/>
        /// <seealso href="http://docs.rackspace.com/cdb/api/v1.0/cdb-devguide/content/DELETE_revokeUserAccess__version___accountId__instances__instanceId__users__name__databases__databaseName__.html">Revoke User Access (Rackspace Cloud Databases Developer Guide - API v1.0)</seealso>
        public static Task RevokeUserAccessAsync(this IDatabaseService service, DatabaseInstanceId instanceId, UserName userName, DatabaseName databaseName, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareRevokeUserAccessAsync(instanceId, userName, databaseName, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        /// <summary>
        /// This method implements support for the <see cref="ReadOnlyCollectionPage{T}.GetNextPageAsync"/> method
        /// for collections returned by the Database Service according to the documentation provided by Rackspace.
        /// </summary>
        /// <remarks>
        /// This method is duplicated from <see cref="DatabaseClient"/>.
        /// </remarks>
        /// <typeparam name="TCall">The type representing the prepared HTTP API call which returns a page of the paginated collection.</typeparam>
        /// <typeparam name="TElement">The type element contained in the paginated collection.</typeparam>
        /// <param name="prepareApiCall">A delegate which asynchronously prepares the HTTP API call to obtain a page of the collection.</param>
        /// <param name="responseObject">The JSON object returned for the previous page of results.</param>
        /// <returns>
        /// A delegate which implements the <see cref="ReadOnlyCollectionPage{T}.GetNextPageAsync"/> behavior
        /// for a paginated collection returned by API calls to this service.
        /// </returns>
        private static Func<CancellationToken, Task<ReadOnlyCollectionPage<TElement>>> CreateGetNextPageAsyncDelegate<TCall, TElement>(Func<CancellationToken, Task<TCall>> prepareApiCall, JObject responseObject)
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
