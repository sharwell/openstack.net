namespace OpenStack.Services.Images.V2
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to <placeholder/>.
    /// </summary>
    /// <seealso cref="ImageTasksExtensions.Prepare"/>
    /// <seealso cref="ImageTasksExtensions."/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class CreateImageApiCall : DelegatingHttpApiCall<Image>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref=""/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public CreateImageApiCall(IHttpApiCall<Image> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
