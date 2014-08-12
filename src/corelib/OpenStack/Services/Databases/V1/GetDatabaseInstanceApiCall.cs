namespace OpenStack.Services.Databases.V1
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to get a database instance.
    /// </summary>
    /// <seealso cref="IDatabaseService.PrepareGetDatabaseInstanceAsync"/>
    /// <seealso cref="DatabaseServiceExtensions.GetDatabaseInstanceAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class GetDatabaseInstanceApiCall : DelegatingHttpApiCall<DatabaseInstanceResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetDatabaseInstanceApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public GetDatabaseInstanceApiCall(IHttpApiCall<DatabaseInstanceResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
