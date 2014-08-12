namespace OpenStack.Services.Databases.V1
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to create a new database instance.
    /// </summary>
    /// <seealso cref="IDatabaseService.PrepareCreateDatabaseInstanceAsync"/>
    /// <seealso cref="DatabaseServiceExtensions.CreateDatabaseInstanceAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class CreateDatabaseInstanceApiCall : DelegatingHttpApiCall<DatabaseInstanceResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateDatabaseInstanceApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public CreateDatabaseInstanceApiCall(IHttpApiCall<DatabaseInstanceResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
