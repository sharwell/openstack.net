namespace OpenStack.Services.Databases.V1
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to get a database flavor.
    /// </summary>
    /// <seealso cref="IDatabaseService.PrepareGetFlavorAsync"/>
    /// <seealso cref="DatabaseServiceExtensions.GetFlavorAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class GetFlavorApiCall : DelegatingHttpApiCall<DatabaseFlavorResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetFlavorApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public GetFlavorApiCall(IHttpApiCall<DatabaseFlavorResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
