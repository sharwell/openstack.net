namespace Rackspace.Services.AutoScale.V1
{
    using System;
    using OpenStack.Net;

    public class GetScalingGroupConfigurationApiCall : DelegatingHttpApiCall<GroupConfigurationResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetScalingGroupConfigurationApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public GetScalingGroupConfigurationApiCall(IHttpApiCall<GroupConfigurationResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
