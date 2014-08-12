namespace OpenStack.Services
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class provides extension methods for simplifying the work of separating
    /// service extensions into separate classes.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    internal static class HttpServiceExtensions
    {
        /// <summary>
        /// Gets an <see cref="IHttpApiCallFactory"/> instance suitable for creating HTTP API
        /// calls that extend a base service client.
        /// </summary>
        /// <param name="service">The service client instance.</param>
        /// <returns>An <see cref="IHttpApiCallFactory"/> instance suitable for creating HTTP API
        /// calls that extend a base service client.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="service"/> is <see langword="null"/>.</exception>
        internal static IHttpApiCallFactory GetHttpApiCallFactory(this IHttpService service)
        {
            IHttpApiCallFactory factory = service as IHttpApiCallFactory;
            return factory ?? new HttpApiCallFactory();
        }
    }
}
