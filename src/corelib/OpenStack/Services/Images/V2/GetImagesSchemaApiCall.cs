namespace OpenStack.Services.Images.V2
{
    using System;
    using Newtonsoft.Json.Schema;
    using OpenStack.Net;

    public class GetImagesSchemaApiCall : DelegatingHttpApiCall<JsonSchema>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref=""/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public GetImagesSchemaApiCall(IHttpApiCall<JsonSchema> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
