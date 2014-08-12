namespace Rackspace.Services.Images.V2
{
    using System;
    using Newtonsoft.Json.Linq;
    using OpenStack.Collections;
    using OpenStack.Net;
    using OpenStack.Services.Images.V2;

    /// <summary>
    /// This class represents a prepared API call to list the ongoing asynchronous tasks
    /// in the <see cref="IImageService"/>.
    /// </summary>
    /// <seealso cref="ImageTasksExtensions.PrepareListTasksAsync"/>
    /// <seealso cref="ImageTasksExtensions.ListTasksAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class ListTasksApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<ImageTask<JObject>>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListTasksApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public ListTasksApiCall(IHttpApiCall<ReadOnlyCollectionPage<ImageTask<JObject>>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
