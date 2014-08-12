namespace Rackspace.Services.Databases.V1
{
    using System;
    using OpenStack.Collections;
    using OpenStack.Net;
    using OpenStack.Services.Databases.V1;

    /// <summary>
    /// This class represents a prepared API call to list the databases a user has access to within a database instance.
    /// </summary>
    /// <seealso cref="RackspaceDatabaseServiceExtensions.PrepareListUserAccessAsync"/>
    /// <seealso cref="RackspaceDatabaseServiceExtensions.ListUserAccessAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class ListUserAccessApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<DatabaseName>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListUserAccessApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public ListUserAccessApiCall(IHttpApiCall<ReadOnlyCollectionPage<DatabaseName>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}