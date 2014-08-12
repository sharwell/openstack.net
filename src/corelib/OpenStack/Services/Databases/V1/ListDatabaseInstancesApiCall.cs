namespace OpenStack.Services.Databases.V1
{
    using System;
    using OpenStack.Collections;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to get a collection of database
    /// instances.
    /// </summary>
    /// <seealso cref="IDatabaseService.PrepareListDatabaseInstancesAsync"/>
    /// <seealso cref="DatabaseServiceExtensions.ListDatabaseInstancesAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class ListDatabaseInstancesApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<DatabaseInstance>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListDatabaseInstancesApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public ListDatabaseInstancesApiCall(IHttpApiCall<ReadOnlyCollectionPage<DatabaseInstance>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
