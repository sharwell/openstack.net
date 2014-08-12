namespace OpenStack.Services.Databases.V1
{
    using System;
    using OpenStack.Collections;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to get a collection of databases within
    /// a database instance.
    /// </summary>
    /// <seealso cref="IDatabaseService.PrepareListDatabasesAsync"/>
    /// <seealso cref="DatabaseServiceExtensions.ListDatabasesAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class ListDatabasesApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<Database>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListDatabasesApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public ListDatabasesApiCall(IHttpApiCall<ReadOnlyCollectionPage<Database>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
