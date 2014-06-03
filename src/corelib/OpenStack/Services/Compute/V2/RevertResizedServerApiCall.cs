namespace OpenStack.Services.Compute.V2
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to revert a server resize operation.
    /// </summary>
    /// <seealso cref="IComputeService.PrepareRevertResizedServerAsync"/>
    /// <seealso cref="ComputeServiceExtensions.RevertResizedServerAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class RevertResizedServerApiCall : DelegatingHttpApiCall<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RevertResizedServerApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public RevertResizedServerApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
