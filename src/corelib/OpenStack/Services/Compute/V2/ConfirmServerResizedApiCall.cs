namespace OpenStack.Services.Compute.V2
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to confirm a server resize operation.
    /// </summary>
    /// <seealso cref="IComputeService.PrepareConfirmServerResizedAsync"/>
    /// <seealso cref="ComputeServiceExtensions.ConfirmServerResizedAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class ConfirmServerResizedApiCall : DelegatingHttpApiCall<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfirmServerResizedApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public ConfirmServerResizedApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
