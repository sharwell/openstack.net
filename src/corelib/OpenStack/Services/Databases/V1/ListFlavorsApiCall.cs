namespace OpenStack.Services.Databases.V1
{
    using System;
    using OpenStack.Collections;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to get a collection of database flavors
    /// available for creating instances.
    /// </summary>
    /// <seealso cref="IDatabaseService.PrepareListFlavorsAsync"/>
    /// <seealso cref="DatabaseServiceExtensions.ListFlavorsAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class ListFlavorsApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<DatabaseFlavor>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListFlavorsApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public ListFlavorsApiCall(IHttpApiCall<ReadOnlyCollectionPage<DatabaseFlavor>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
