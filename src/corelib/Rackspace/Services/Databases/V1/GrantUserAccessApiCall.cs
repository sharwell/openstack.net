namespace Rackspace.Services.Databases.V1
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to grant a user access to particular databases within a database instance.
    /// </summary>
    /// <seealso cref="RackspaceDatabaseServiceExtensions.PrepareGrantUserAccessAsync"/>
    /// <seealso cref="RackspaceDatabaseServiceExtensions.GrantUserAccessAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class GrantUserAccessApiCall : DelegatingHttpApiCall<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GrantUserAccessApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public GrantUserAccessApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}