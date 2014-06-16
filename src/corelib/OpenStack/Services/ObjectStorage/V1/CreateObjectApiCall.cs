namespace OpenStack.Services.ObjectStorage.V1
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to create or replace an object.
    /// </summary>
    /// <seealso cref="IObjectStorageService.PrepareCreateObjectAsync"/>
    /// <seealso cref="ObjectStorageServiceExtensions.CreateObjectAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class CreateObjectApiCall : DelegatingHttpApiCall<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateObjectApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public CreateObjectApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
