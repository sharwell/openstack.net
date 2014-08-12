namespace Rackspace.Services.Images.V2
{
    using System;
    using OpenStack.Net;
    using OpenStack.Services.Images.V2;

    /// <summary>
    /// This class represents a prepared API call to asynchronously export an image from
    /// the <see cref="IImageService"/>.
    /// </summary>
    /// <seealso cref="ImageTasksExtensions.PrepareExportImageAsync"/>
    /// <seealso cref="ImageTasksExtensions.ExportImageAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class ExportImageApiCall : DelegatingHttpApiCall<ImageTask<ExportTaskInput>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExportImageApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public ExportImageApiCall(IHttpApiCall<ImageTask<ExportTaskInput>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}