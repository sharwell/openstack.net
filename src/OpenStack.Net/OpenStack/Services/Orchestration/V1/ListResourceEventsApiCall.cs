namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using OpenStack.Collections;
    using OpenStack.Net;

    /// <summary>
    /// This class represents an HTTP API call to list <see cref="Event"/> resource associated with a
    /// <see cref="Resource"/> in the OpenStack Orchestration Service.
    /// </summary>
    /// <seealso cref="IOrchestrationService.PrepareListResourceEventsAsync"/>
    /// <seealso cref="OrchestrationServiceExtensions.ListResourceEventsAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class ListResourceEventsApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<Event>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListResourceEventsApiCall"/> class with the behavior
        /// provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="httpApiCall"/> is <see langword="null"/>.
        /// </exception>
        public ListResourceEventsApiCall(IHttpApiCall<ReadOnlyCollectionPage<Event>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
