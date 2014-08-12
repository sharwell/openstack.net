namespace OpenStack.Services.Databases.V1
{
    using System;
    using OpenStack.Collections;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to get a collection of users in a database instance.
    /// </summary>
    /// <seealso cref="IDatabaseService.PrepareListUsersAsync"/>
    /// <seealso cref="DatabaseServiceExtensions.ListUsersAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class ListUsersApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<DatabaseUser>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListUsersApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public ListUsersApiCall(IHttpApiCall<ReadOnlyCollectionPage<DatabaseUser>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
