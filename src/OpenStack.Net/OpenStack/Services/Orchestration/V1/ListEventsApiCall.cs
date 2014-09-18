namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using OpenStack.Collections;
    using OpenStack.Net;

    /// <summary>
    /// This class represents an HTTP API call to list <see cref="Event"/> resources associated with a
    /// <see cref="Stack"/> in the OpenStack Orchestration Service.
    /// </summary>
    /// <seealso cref="IOrchestrationService.PrepareListEventsAsync"/>
    /// <seealso cref="OrchestrationServiceExtensions.ListEventsAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class ListEventsApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<Event>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListEventsApiCall"/> class with the behavior
        /// provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="httpApiCall"/> is <see langword="null"/>.
        /// </exception>
        public ListEventsApiCall(IHttpApiCall<ReadOnlyCollectionPage<Event>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
