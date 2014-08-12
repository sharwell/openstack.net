namespace Rackspace.Services.Images.V2
{
    using System;
    using OpenStack.Net;
    using OpenStack.Services.Images.V2;

    /// <summary>
    /// This class represents a prepared API call to asynchronously import an image into
    /// the <see cref="IImageService"/>.
    /// </summary>
    /// <seealso cref="ImageTasksExtensions.PrepareImportImageAsync"/>
    /// <seealso cref="ImageTasksExtensions.ImportImageAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class ImportImageApiCall : DelegatingHttpApiCall<ImageTask<ImportTaskInput>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImportImageApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public ImportImageApiCall(IHttpApiCall<ImageTask<ImportTaskInput>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}