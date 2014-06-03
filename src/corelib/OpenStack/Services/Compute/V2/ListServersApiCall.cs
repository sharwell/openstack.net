namespace OpenStack.Services.Compute.V2
{
    using System;
    using OpenStack.Collections;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to list the server resources in the
    /// <see cref="IComputeService"/>.
    /// </summary>
    /// <seealso cref="IComputeService.PrepareListServersAsync"/>
    /// <seealso cref="ComputeServiceExtensions.ListServersAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class ListServersApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<Server>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListServersApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public ListServersApiCall(IHttpApiCall<ReadOnlyCollectionPage<Server>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
