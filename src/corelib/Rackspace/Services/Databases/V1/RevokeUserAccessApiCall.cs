namespace Rackspace.Services.Databases.V1
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to revoke a user's access to a database within a database instance.
    /// </summary>
    /// <seealso cref="RackspaceDatabaseServiceExtensions.PrepareRevokeUserAccessAsync"/>
    /// <seealso cref="RackspaceDatabaseServiceExtensions.RevokeUserAccessAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class RevokeUserAccessApiCall : DelegatingHttpApiCall<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RevokeUserAccessApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public RevokeUserAccessApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}