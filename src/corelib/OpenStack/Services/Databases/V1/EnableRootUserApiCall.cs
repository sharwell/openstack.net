namespace OpenStack.Services.Databases.V1
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to enable the root user account in
    /// a database instance.
    /// </summary>
    /// <seealso cref="IDatabaseService.PrepareEnableRootUserAsync"/>
    /// <seealso cref="DatabaseServiceExtensions.EnableRootUserAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class EnableRootUserApiCall : DelegatingHttpApiCall<DatabaseUserResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnableRootUserApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public EnableRootUserApiCall(IHttpApiCall<DatabaseUserResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
