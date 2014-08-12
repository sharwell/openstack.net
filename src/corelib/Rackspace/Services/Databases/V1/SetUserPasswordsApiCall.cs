namespace Rackspace.Services.Databases.V1
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to set the password for one or more users in a database instance.
    /// </summary>
    /// <seealso cref="RackspaceDatabaseServiceExtensions.PrepareSetUserPasswordsAsync"/>
    /// <seealso cref="RackspaceDatabaseServiceExtensions.SetUserPasswordsAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class SetUserPasswordsApiCall : DelegatingHttpApiCall<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetUserPasswordsApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public SetUserPasswordsApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}