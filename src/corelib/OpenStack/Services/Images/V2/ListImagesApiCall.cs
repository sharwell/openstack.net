namespace OpenStack.Services.Images.V2
{
    using System;
    using OpenStack.Collections;
    using OpenStack.Net;

    public class ListImagesApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<Image>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref=""/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public ListImagesApiCall(IHttpApiCall<ReadOnlyCollectionPage<Image>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
