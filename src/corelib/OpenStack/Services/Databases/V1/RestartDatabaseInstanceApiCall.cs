namespace OpenStack.Services.Databases.V1
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to restart a database instance.
    /// </summary>
    /// <seealso cref="IDatabaseService.PrepareRestartDatabaseInstanceAsync"/>
    /// <seealso cref="DatabaseServiceExtensions.RestartDatabaseInstanceAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class RestartDatabaseInstanceApiCall : DelegatingHttpApiCall<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RestartDatabaseInstanceApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public RestartDatabaseInstanceApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
