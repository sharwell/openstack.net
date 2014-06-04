namespace OpenStack.Services.Compute.V2
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to change the password
    /// of a server resource.
    /// </summary>
    /// <seealso cref="IComputeService.PrepareChangePasswordAsync"/>
    /// <seealso cref="ComputeServiceExtensions.ChangePasswordAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class ChangePasswordApiCall : DelegatingHttpApiCall<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public ChangePasswordApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}